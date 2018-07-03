using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
	public enum Gamestate
	{
		Idle,
		Tracked,
		Finished
	}

	public static GameManager Instance;

	public delegate void KeyAction();
	public KeyAction KeyCollected;

	[SerializeField] private GameObject _turnBoardArrow;
	[SerializeField] private Animator _chestAnimator;
	[SerializeField] private PlayableDirector _director;
	[SerializeField] private bool _startInLevelTwo;

	private Gamestate _state = Gamestate.Idle;
	public Gamestate State => _state;
	private bool _firstLevelDone;

	private void OnEnable()
	{
		DefaultTrackableEventHandler.OnTrackingEvent += OnTracking;
	}

	private void OnDisable()
	{
		DefaultTrackableEventHandler.OnTrackingEvent -= OnTracking;
	}

	private void OnTracking(bool trackingFound)
	{
		_state = trackingFound ? Gamestate.Tracked : Gamestate.Idle;
		OnStateChanged();
		
		if(_firstLevelDone && trackingFound && _turnBoardArrow.gameObject.activeSelf)
			_turnBoardArrow.SetActive(false);
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	private void Start()
	{
		if (_startInLevelTwo)
		{
			LevelDone();
		}
	}

	private void OnStateChanged()
	{
		switch (_state)
		{
			case Gamestate.Idle:
				break;
			case Gamestate.Tracked:
				SpawnManager.Instance.Respawn();
				break;
			case Gamestate.Finished:
				StartCoroutine(PopulateHint());
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public void LevelDone()
	{
		if (!_firstLevelDone)
		{
			_director.gameObject.SetActive(true);
			_director.Play();
			StartCoroutine(AudioManager.Instance.NextLevel());
			_turnBoardArrow.SetActive(true);
			_firstLevelDone = true;
			SpawnManager.Instance.SwapLevel(1);
		}
		else
		{
			Rigidbody rigidbodyPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
			rigidbodyPlayer.velocity = Vector3.zero;
			rigidbodyPlayer.useGravity = false;

			_state = Gamestate.Finished;
			OnStateChanged();
		}
	}

	private IEnumerator PopulateHint()
	{
		_chestAnimator.SetTrigger("PopulateHint");
		SFXManager.Instance.PlaySFX(SFXManager.Instance.ChestOpenSound);
		yield return new WaitForSeconds(SFXManager.Instance.ChestOpenSound.length);
		SFXManager.Instance.PlaySFX(SFXManager.Instance.WinSound);

		Debug.Log("Game Done.");
	}
}

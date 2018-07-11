using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Vuforia;
using _4_Kugellabyrinth._Kevin.Objects;
using _4_Kugellabyrinth._Kevin.Player;

namespace _4_Kugellabyrinth._Kevin
{
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

		[SerializeField] private Vector3 _customPhysicsVector = new Vector3(0, -9.81f, 0);
		[SerializeField] private GameObject _turnBoardArrow;
		[SerializeField] private Animator _chestAnimator;
		[SerializeField] private PlayableDirector _director;
		[SerializeField] private bool _startInLevelTwo;
		[SerializeField] private GameObject _warningMessage;
		[SerializeField] private LevelData _levelData;
		[SerializeField] private GameObject _goalParticles;
		[SerializeField] private TrackableBehaviour _gravityOrigin;

		private Gamestate _state = Gamestate.Idle;
		public Gamestate State => _state;
		private bool _firstLevelDone;

		private void OnEnable()
		{
			DefaultTrackableEventHandler.OnSceneTracking += OnTracking;
		}

		private void OnDisable()
		{
			DefaultTrackableEventHandler.OnSceneTracking -= OnTracking;
		}

		private void OnTracking(bool trackingFound, TrackableBehaviour trackable)
		{
			if (!_firstLevelDone && !_startInLevelTwo && trackingFound && trackable.name.Equals("Level2"))
			{
				DisplayWarning();
				return;
			}

			if(trackable.name.Equals("Level2") && trackingFound)
				_levelData.SetActive(true);

			if (_warningMessage.activeSelf)
				_warningMessage.SetActive(false);

			_state = trackingFound ? Gamestate.Tracked : Gamestate.Idle;
			OnStateChanged();
		
			if(_firstLevelDone && trackingFound && _turnBoardArrow.gameObject.activeSelf)
				_turnBoardArrow.SetActive(false);
		}

		private void DisplayWarning()
		{
			_warningMessage.SetActive(true);
			_levelData.SetActive(false);
		}

		private void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;

			Door.IsOpen = false;
			Physics.gravity = _customPhysicsVector;
			_turnBoardArrow = GameObject.Find("ARCamera").GetComponentInChildren<TurnBoardArrow>(true).gameObject;
			VuforiaARController.Instance.SetWorldCenterMode(VuforiaARController.WorldCenterMode.SPECIFIC_TARGET);
			VuforiaARController.Instance.SetWorldCenter(_gravityOrigin);
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

		//Skip Level
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				_firstLevelDone = true;
				Door.IsOpen = true;
				LevelDone();
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
				if (!Door.IsOpen) return;
				_goalParticles.SetActive(false);

				Rigidbody rigidbodyPlayer = GameObject.FindWithTag("Player")?.GetComponent<Rigidbody>();
				if (rigidbodyPlayer)
				{
					rigidbodyPlayer.velocity = Vector3.zero;
					rigidbodyPlayer.useGravity = false;
				}

				_state = Gamestate.Finished;
				OnStateChanged();
			}
		}

		private IEnumerator PopulateHint()
		{
			_levelData.SetActive(false);
			_chestAnimator.transform.parent.gameObject.SetActive(true);
			_chestAnimator.SetTrigger("PopulateHint");
			SFXManager.Instance.PlaySFX(SFXManager.Instance.ChestOpenSound);
			yield return new WaitForSeconds(SFXManager.Instance.ChestOpenSound.length + 1f);
			SFXManager.Instance.PlaySFX(SFXManager.Instance.WinSound);

			VuforiaARController.Instance.SetWorldCenterMode(VuforiaARController.WorldCenterMode.FIRST_TARGET);
			MultisceneManager.Instance.StartCoroutine(MultisceneManager.Instance.FinishLevel(true));
			Debug.Log("Game Done.");
		}

		[Serializable]
		private class LevelData
		{
			[SerializeField] private GameObject _environmentObject;
			[SerializeField] private Player.PlayerController _player;

			public void SetActive(bool active)
			{
				_environmentObject.SetActive(active);
				_player.gameObject.SetActive(active);
			}
		}
	}
}

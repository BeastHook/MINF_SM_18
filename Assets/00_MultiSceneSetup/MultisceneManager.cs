using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vuforia;

public class MultisceneManager : MonoBehaviour
{
	public static MultisceneManager Instance;

	[SerializeField] private int _gameStartsInTime = 3;
	[SerializeField] private float _maximumDistanceToTrackable = 30f;
	[SerializeField] private int _afterLevelWaitTime = 6;
	[SerializeField, FormerlySerializedAs("_minfScenes")] private Minigame[] _minigames;

	private Minigame _currentMinigame;
	private bool _hasAdditionalScene;

	private void Awake()
	{
		if ((Instance != this) && (Instance != null))
			Destroy(gameObject);
		else
			Instance = this;

		DontDestroyOnLoad(gameObject);

		DefaultTrackableEventHandler.OnSceneTracking += OnSceneTracking;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		SceneManager.sceneLoaded += OnSceneLoaded;

		SetAllDescriptionCanvasses();
	}

	private void SetAllDescriptionCanvasses()
	{
		foreach (Minigame minigame in _minigames)
		{
			minigame.DescriptionCanvas.InitiliazeWithMinigame(minigame);
		}
	}

	//To avoid triggering when the master minigame initially loads, we check if the build index equals 0 (=> put master minigame to build index 0!)
	private void OnSceneUnloaded(Scene unloadedScene)
	{
		if (unloadedScene.buildIndex != 0)
		{
			_hasAdditionalScene = false;
			if (_currentMinigame != null)
			{
				if (_currentMinigame.AccordingTrackable != null)
				{
					_currentMinigame.AccordingTrackable.gameObject.SetActive(true);
					_currentMinigame.AccordingTrackable.enabled = true;
				}
			}
			_currentMinigame = null;
		}
	}

	private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
	{
		TrackerManager.Instance.GetStateManager().ReassociateTrackables();
		if (loadedScene.buildIndex != 0 && loadMode == LoadSceneMode.Additive)
		{
			StartCoroutine(LevelTimer(_currentMinigame.MaximumPlaytime));
		}
	}

	private void OnDestroy()
	{
		DefaultTrackableEventHandler.OnSceneTracking -= OnSceneTracking;
	}

	private void OnSceneTracking(bool trackingfound, TrackableBehaviour behaviour)
	{
		if(_hasAdditionalScene) return;

		if (!trackingfound)
		{
			StopAllCoroutines();
			return;
		}

		Vector3 position = Camera.main.transform.position;
		float distance = position.magnitude;

		if(distance > _maximumDistanceToTrackable) return;

		foreach (Minigame minigame in _minigames)
		{
			if (behaviour.TrackableName != minigame.AccordingTrackable.TrackableName || minigame.WasPlayedAlready) continue;

			StartCoroutine(StartGameCountdown(minigame));
			break;
		}
	}

	private void LoadMinigame(Minigame minigame)
	{
		SceneManager.LoadScene(minigame.Scene, LoadSceneMode.Additive);
		_hasAdditionalScene = true;
		_currentMinigame = minigame;
		_currentMinigame.WasPlayedAlready = true;
	}

	private IEnumerator StartGameCountdown(Minigame minigame)
	{
		int currentCountdown = _gameStartsInTime;
		while (currentCountdown > 0)
		{
			minigame.DescriptionCanvas.UpdateStartsIn(currentCountdown);
			yield return new WaitForSeconds(1f);
			currentCountdown--;
		}
		minigame.AccordingTrackable.gameObject.SetActive(false);
		minigame.AccordingTrackable.enabled = false;
		LoadMinigame(minigame);
	}

	private IEnumerator LevelTimer(int time)
	{
		while (time > 0)
		{
			yield return new WaitForSeconds(1f);
			if(!_hasAdditionalScene)
				yield break; //Leave the coroutine, if the level gets finished before the countdown has ended
			time--;
		}

		StartCoroutine(FinishLevel(false));
	}

	//Call this in your game, when your player has won/lost the game (with according bool flag)
	public IEnumerator FinishLevel(bool hasWon)
	{
		yield return new WaitForSeconds(_afterLevelWaitTime);
		SceneManager.UnloadSceneAsync(_currentMinigame.Scene);
		_currentMinigame.DescriptionCanvas.UpdateCanvas(hasWon);
	}

	[Serializable]
	public class Minigame
	{
		[SerializeField] private SceneField _scene;
		[SerializeField, Tooltip("Maximum Playtime in seconds")] private int _maximumPlaytime = 240;
		[SerializeField] private TrackableBehaviour _accordingTrackable;
		[Header("For 3D-UI")]
		[SerializeField] private MinigameCanvas _descriptionCanvas;
		[SerializeField] private string _name;
		[SerializeField] private string _description;
		[SerializeField] private Sprite _sprite;
		[SerializeField] private GameObject _solutionLetters;

		public bool WasPlayedAlready { get; set; }

		public TrackableBehaviour AccordingTrackable => _accordingTrackable;
		public SceneField Scene => _scene;
		public int MaximumPlaytime => _maximumPlaytime;
		public MinigameCanvas DescriptionCanvas => _descriptionCanvas;
		public GameObject SolutionLetters => _solutionLetters;
		public string Name => _name;
		public string Description => _description;
		public Sprite @Sprite => _sprite;
	}
}

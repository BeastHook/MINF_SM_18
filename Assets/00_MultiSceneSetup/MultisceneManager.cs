using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vuforia;
using wizardsduelvr.networking;

public class MultisceneManager : MonoBehaviour
{

	[SerializeField] private float _maximumDistanceToTrackable = 2f;
	[SerializeField] private int _afterLevelWaitTime;
	[SerializeField, FormerlySerializedAs("_minfScenes")] private Minigame[] _minigames;
	[SerializeField] private Camera _vuforiaCamera;

	private Minigame _currentMinigame;
	private bool _hasAdditionalScene;

	[Serializable]
	private class Minigame
	{
		[SerializeField] private SceneField _scene;
		[SerializeField, Tooltip("Maximum Playtime in seconds")] private int _maximumPlaytime = 240;

		[SerializeField] private TrackableBehaviour _accordingTrackable;

		public TrackableBehaviour AccordingTrackable
		{
			get { return _accordingTrackable; }
		}

		public SceneField Scene
		{
			get { return _scene; }
		}

		public int MaximumPlaytime
		{
			get { return _maximumPlaytime; }
		}
	}

	private void Awake()
	{
		DefaultTrackableEventHandler.OnSceneTracking += OnSceneTracking;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	//To avoid triggering when the master minigame initially loads, we check if the build index equals 0 (=> put master minigame to build index 0!)
	private void OnSceneUnloaded(Scene unloadedScene)
	{
		if (unloadedScene.buildIndex != 0)
		{
			_vuforiaCamera.gameObject.SetActive(true);
			_hasAdditionalScene = false;
			//_currentMinigame.AccordingTrackable.gameObject.SetActive(true);
		}
	}

	private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
	{
		if (loadedScene.buildIndex != 0 && loadMode == LoadSceneMode.Additive)
		{
			//_vuforiaCamera.gameObject.SetActive(false);
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

		Vector3 position = Camera.main.transform.position;
		float distance = position.magnitude;

		if(distance > _maximumDistanceToTrackable)
			return;

		foreach (Minigame minigame in _minigames)
		{
			if (behaviour.TrackableName != minigame.AccordingTrackable.TrackableName) continue;

			_hasAdditionalScene = true;
			minigame.AccordingTrackable.gameObject.SetActive(false);
			minigame.AccordingTrackable.enabled = false;
			StartScene(minigame);
			break;
		}
	}

	private void StartScene(Minigame minigame)
	{
		SceneManager.LoadScene(minigame.Scene, LoadSceneMode.Additive);
		_hasAdditionalScene = true;
		_currentMinigame = minigame;
	}

	private IEnumerator LevelTimer(int time)
	{
		yield return new WaitForSeconds(time);
		SceneManager.UnloadSceneAsync(_currentMinigame.Scene);
	}

	private IEnumerator OnLevelFinished()
	{
		yield return new WaitForSeconds(_afterLevelWaitTime);
		SceneManager.UnloadSceneAsync(_currentMinigame.Scene);
	}
}

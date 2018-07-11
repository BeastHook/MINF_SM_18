using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTracker : MonoBehaviour
{

	private bool _gazeOnObject;

	private float _timeToTrack = 1.5f;
	private float _currentTrackingTime;

	private void Awake()
	{
		_currentTrackingTime = 0.0f;
		DontDestroyOnLoad(gameObject);
	}

	private void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay(Camera.main.ViewportToScreenPoint(Vector3.zero));
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.origin, Color.blue, 3.0f);
		if (Physics.Raycast(ray, out hit))
		{
			_gazeOnObject = hit.distance >= 20.0f;
			Debug.Log("hit something");
		}
		else
		{
			_gazeOnObject = false;
		}

		if (!_gazeOnObject) return;

		_currentTrackingTime += Time.deltaTime;

		if (_currentTrackingTime > _timeToTrack)
		{
			Debug.Log("Tracked an Object!");
		}
	}
}

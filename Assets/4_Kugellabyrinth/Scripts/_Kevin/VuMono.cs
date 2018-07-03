using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuMono : MonoBehaviour
{
	protected Transform _cachedTransform;
	public Transform CachedTransform => _cachedTransform;

	protected virtual void Awake()
	{
		_cachedTransform = transform;
		DefaultTrackableEventHandler.OnTrackingEvent += OnTracking;
	}

	protected void OnTracking(bool trackingfound)
	{
		gameObject.SetActive(trackingfound);
	}
}

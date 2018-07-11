using UnityEngine;
using Vuforia;

namespace _4_Kugellabyrinth._Kevin
{
	public class VuMono : MonoBehaviour
	{
		protected Transform _cachedTransform;
		public Transform CachedTransform => _cachedTransform;

		protected virtual void Awake()
		{
			_cachedTransform = transform;
			DefaultTrackableEventHandler.OnSceneTracking += OnTracking;
		}

		protected virtual void OnTracking(bool trackingfound, TrackableBehaviour trackable)
		{
			if (!trackable.name.Equals(_cachedTransform.root.name)) return;

			gameObject.SetActive(trackingfound);
		}

		private void OnDestroy()
		{
			DefaultTrackableEventHandler.OnSceneTracking -= OnTracking;
		}
	}
}

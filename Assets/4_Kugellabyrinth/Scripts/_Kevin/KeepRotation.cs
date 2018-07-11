using UnityEngine;
using Vuforia;

namespace _4_Kugellabyrinth._Kevin
{
	public class KeepRotation : VuMono
	{
		[SerializeField] private TrackableBehaviour _otherTrackable;
		private Vector3 _offset;
		private Quaternion _startRotation;

		protected override void Awake()
		{
			base.Awake();
			_offset = CachedTransform.position - _otherTrackable.gameObject.transform.position;
			_startRotation = CachedTransform.rotation;
		}

		private void LateUpdate()
		{
			CachedTransform.rotation = _startRotation;
			CachedTransform.position = _otherTrackable.gameObject.transform.position + _offset;
		}
	}
}

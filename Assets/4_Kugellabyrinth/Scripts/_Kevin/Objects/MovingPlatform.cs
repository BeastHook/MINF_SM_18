using DG.Tweening;
using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Objects
{
	public class MovingPlatform : VuMono
	{
		[SerializeField] private Transform _targetPosition;
		[SerializeField] private AnimationCurve _easingCurve;
		[SerializeField] private float _movementDuration;
		[SerializeField] private float _waitTime;
		
		private Vector3 _startPoint;
		private Sequence _movementSequence;

		protected override void Awake()
		{
			base.Awake();
			
			Initialize();
		}

		private void Initialize()
		{
			_startPoint = _cachedTransform.localPosition;

			_movementSequence = DOTween.Sequence();
			_movementSequence
				.Append(_cachedTransform.DOLocalMove(_targetPosition.localPosition, _movementDuration).SetEase(_easingCurve))
				.AppendInterval(_waitTime)
				.PrependInterval(_waitTime)
				.Append(_cachedTransform.DOLocalMove(_startPoint, _movementDuration).SetEase(_easingCurve))
				.OnComplete(() => _movementSequence.Restart());
		}
	}
}

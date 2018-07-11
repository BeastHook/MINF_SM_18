using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _4_Kugellabyrinth._Kevin.Objects
{
	public class TurnBoardArrow : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("_ZPosCurve")] private AnimationCurve _zPosCurve;
		[SerializeField, FormerlySerializedAs("_YPosCurve")] private AnimationCurve _yPosCurve;
		[SerializeField] private AnimationCurve _rotCurve;

		private Vector3 _startPosition;
		private Vector3 _startRotation;

		private void Awake()
		{
			_startPosition = transform.position;
			_startRotation = transform.eulerAngles;
		}

		private void Update()
		{
			if (DOTween.IsTweening(transform)) return;
			StartCoroutine(Movement());
		}

		private IEnumerator Movement()
		{
			transform.DOLocalMoveZ(_startPosition.x + 35, 2.5f)
				.SetEase(_zPosCurve)
				.OnStart(() =>
				         {
					         transform.DOLocalMoveY(_startPosition.y + 10, 2.5f).SetEase(_yPosCurve);
					         transform.DOLocalRotate(new Vector3(_startRotation.x, _startRotation.y, _startRotation.z), 2.25f).SetEase(_rotCurve);
				         })
				.OnComplete(() =>
				            {
					            transform.position = _startPosition;
					            transform.eulerAngles = _startRotation;
				            });
			yield return null;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

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
		transform.DOMoveZ(_startPosition.z + 25, 1.5f)
			.SetEase(_zPosCurve)
			.OnStart(() =>
				{
					transform.DOMoveY(_startPosition.y + 10, 1.5f).SetEase(_yPosCurve);
					transform.DORotate(new Vector3(_startRotation.x + 85, _startRotation.y, _startRotation.z), 1.25f).SetEase(_rotCurve);
				})
			.OnComplete(() =>
			{
				transform.position = _startPosition;
				transform.eulerAngles = _startRotation;
			});
		yield return null;
	}
}

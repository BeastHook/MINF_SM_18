using DG.Tweening;
using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Objects
{ 
	public class Button : VuMono
	{
		[SerializeField] private Transform[] _counterPart;
		[SerializeField] private Transform _sinkInPosition;
		[SerializeField] private Transform[] _activatedTargetPosition;

		private bool _pressed;

		protected override void Awake()
		{
			base.Awake();
			foreach (Transform t in _counterPart)
			{
				t.gameObject.SetActive(false);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && !_pressed)
			{
				Activate();
			}
		}

		private void Activate()
		{
			SFXManager.Instance.PlaySFX(SFXManager.Instance.ButtonPressedSound);
			if (_sinkInPosition != null)
				transform.DOLocalMove(_sinkInPosition.localPosition, 0.5f).OnComplete(OnComplete);
			else
				OnComplete();
		}

		private void OnComplete()
		{
			for (int i = 0; i < _counterPart.Length; i++)
			{
				_counterPart[i].gameObject.SetActive(true);
				_counterPart[i].DOLocalMove(_activatedTargetPosition[i].localPosition, 1.5f);
				_pressed = true;
			}
		}
	}
}

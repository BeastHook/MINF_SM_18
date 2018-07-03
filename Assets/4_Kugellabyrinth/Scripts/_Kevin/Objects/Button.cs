using DG.Tweening;
using UnityEngine;

namespace Objects
{ 
	public class Button : VuMono
	{
		[SerializeField] private Transform _counterPart;
		[SerializeField] private Transform _sinkInPosition;

		private Transform _activatedTargetPosition;

		protected override void Awake()
		{
			base.Awake();
			_counterPart.gameObject.SetActive(false);
			_activatedTargetPosition = _counterPart.GetChild(0);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				Activate();
			}
		}

		private void Activate()
		{
			transform.DOMove(_sinkInPosition.position, 0.5f).OnComplete(() =>
			{
				SFXManager.Instance.PlaySFX(SFXManager.Instance.ButtonPressedSound);
				_counterPart.gameObject.SetActive(true);
				_counterPart.DOMove(_activatedTargetPosition.position, 1.5f);
			});
		}
	}
}

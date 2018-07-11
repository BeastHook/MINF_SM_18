using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Objects
{
	public class Key : VuMono
	{
		[SerializeField] private float _rotationSpeed;
		private bool _collected;

		private void Start()
		{
			_cachedTransform = transform;
		}

		private void OnEnable()
		{
			if(_collected)
				gameObject.SetActive(false);
		}

		// Update is called once per frame
		private void Update()
		{
			float rot = _rotationSpeed * Time.deltaTime * 15f;
			_cachedTransform.Rotate(rot, rot, rot);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && !_collected)
			{
				GameManager.Instance.KeyCollected?.Invoke();
				SFXManager.Instance.PlaySFX(SFXManager.Instance.CollectSound);
				gameObject.SetActive(false);
				_collected = true;
			}
		}
	}
}

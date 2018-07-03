using UnityEngine;

namespace Objects
{
	public class Key : VuMono
	{
		[SerializeField] private float _rotationSpeed;

		private void Start()
		{
			_cachedTransform = transform;
		}

		// Update is called once per frame
		private void Update ()
		{
			float rot = _rotationSpeed * Time.deltaTime * 15f;
			_cachedTransform.Rotate(rot, rot, rot);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				GameManager.Instance.KeyCollected?.Invoke();
				SFXManager.Instance.PlaySFX(SFXManager.Instance.CollectSound);
			}
		}
	}
}

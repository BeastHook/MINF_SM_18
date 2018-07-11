using UnityEngine;
using Vuforia;

namespace _4_Kugellabyrinth._Kevin.Player
{
	public class PlayerController : VuMono {
		
		[SerializeField, Range(0.05f, 2.0f)] private float _heightOnRespawn = 1.5f;

		private Rigidbody _rigidBody;
		private Collider _collider;

		private bool _isChasable;
		public bool IsChasable => _isChasable;

		protected override void Awake()
		{
			base.Awake();

			gameObject.SetActive(false);

			_rigidBody = GetComponentInChildren<Rigidbody>();
			_collider = GetComponent<Collider>();
			_isChasable = false;

			SpawnManager.OnRespawn += Respawn;
		}

		private void OnDestroy()
		{
			SpawnManager.OnRespawn -= Respawn;
		}

		private void Update()
		{
			if(_cachedTransform.localPosition.y < -5f)
				SpawnManager.Instance.Respawn();
		}

		protected override void OnTracking(bool trackingfound, TrackableBehaviour trackable)
		{
			if(!trackable.name.Equals(_cachedTransform.root.name)) return;

			Toggle(trackingfound);
		}

		public void Toggle(bool on)
		{
			gameObject.SetActive(on);
			_collider.enabled = on;
			_rigidBody.useGravity = on;

			if(!on)
				_rigidBody.velocity = Vector3.zero;
		}

		private void Respawn(Transform spawnpoint)
		{
			_isChasable = false;
			_rigidBody.velocity = Vector3.zero;
			transform.position = spawnpoint.position;
			transform.position += Vector3.up * _heightOnRespawn;
			SFXManager.Instance.PlaySFX(SFXManager.Instance.RespawnSound);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("ChaseZoneTrigger"))
			{
				_isChasable = !_isChasable;
			}
		}
	}
}

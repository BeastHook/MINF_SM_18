using DG.Tweening;
using UnityEngine;

namespace Player
{
	public class PlayerController : MonoBehaviour {

		[SerializeField] private float _speed = 1.0f;
		[SerializeField, Range(0.05f, 2.0f)] private float _heightOnRespawn = 1.5f;

		private Rigidbody _rigidBody;
		private Collider _collider;

		private bool _isChasable;
		public bool IsChasable => _isChasable;

		private void Awake()
		{
			gameObject.SetActive(false);

			_rigidBody = GetComponentInChildren<Rigidbody>();
			_collider = GetComponent<Collider>();
			_isChasable = false;

			SpawnManager.OnRespawn += Respawn;
			DefaultTrackableEventHandler.OnTrackingEvent += OnTracking;
		}

		private void OnDestroy()
		{
			SpawnManager.OnRespawn -= Respawn;
			DefaultTrackableEventHandler.OnTrackingEvent -= OnTracking;
		}

		private void OnTracking(bool trackingfound)
		{
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
			transform.position += Vector3.back * _heightOnRespawn;
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

using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Traps
{
	public class WindTrap : Trap
	{
		[SerializeField] private Vector3 _blowDirection;
		[SerializeField] private float _windForce = 1f;
		private ParticleSystem _windParticleSystem;

		protected override void Awake()
		{
			base.Awake();

			_windParticleSystem = GetComponentInChildren<ParticleSystem>();
			_blowDirection.Normalize();
		}

		protected override void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && _canDamage)
			{
				other.attachedRigidbody.AddForce(_blowDirection * _windForce);
			}
		}

		protected override void DisableDamage()
		{
			base.DisableDamage();
			_windParticleSystem.Stop();
		}

		protected override void StartTrap()
		{
			base.StartTrap();
			_windParticleSystem.Play();
		}
	}
}

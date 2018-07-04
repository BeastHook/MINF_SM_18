using UnityEngine;

namespace Objects
{
	public class Door : VuMono
	{

		private Animator _animator;

		private void Start()
		{
			_animator = GetComponent<Animator>();
			GameManager.Instance.KeyCollected += OpenDoor;
		}

		private void OnDestroy()
		{
			GameManager.Instance.KeyCollected -= OpenDoor;
		}

		private void OpenDoor()
		{
			_animator?.SetBool("Open", true);
			SFXManager.Instance.PlaySFX(SFXManager.Instance.DoorOpenSound);
		}
	}
}

using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Objects
{
	public class Door : VuMono
	{
		public static bool IsOpen = false;
		private Animator _animator;

		private void Start()
		{
			_animator = GetComponent<Animator>();
			GameManager.Instance.KeyCollected += OpenDoor;
		}

		private void OnEnable()
		{
			if(IsOpen)
				gameObject.SetActive(false);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			GameManager.Instance.KeyCollected -= OpenDoor;
		}

		private void OpenDoor()
		{
			_animator?.SetBool("Open", true);
			SFXManager.Instance.PlaySFX(SFXManager.Instance.DoorOpenSound);
			IsOpen = true;
		}
	}
}

using UnityEngine;

namespace _4_Kugellabyrinth._Kevin
{
	[RequireComponent(typeof(AudioSource))]
	public class SFXManager : MonoBehaviour
	{
		public static SFXManager Instance;

		public AudioClip HitSound => _hitSound;
		public AudioClip RespawnSound => _respawnSound;
		public AudioClip QuickSandDragSound => _quickSandDragSound;
		public AudioClip QuickSandSpitSound => _quickSandSpitSound;
		public AudioClip CollectSound => _collectSound;
		public AudioClip ButtonPressedSound => _buttonPressedSound;
		public AudioClip DoorOpenSound => _doorOpenSound;
		public AudioClip ChestOpenSound => _chestOpenSound;
		public AudioClip WinSound => _winSound;

		[SerializeField] private AudioClip _hitSound;
		[SerializeField] private AudioClip _respawnSound;
		[SerializeField] private AudioClip _quickSandDragSound;
		[SerializeField] private AudioClip _quickSandSpitSound;
		[SerializeField] private AudioClip _collectSound;
		[SerializeField] private AudioClip _buttonPressedSound;
		[SerializeField] private AudioClip _doorOpenSound;
		[SerializeField] private AudioClip _chestOpenSound;
		[SerializeField] private AudioClip _winSound;

		private AudioSource _source;

		private void Awake()
		{
			if (Instance != this && Instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = this;
			}

			_source = GetComponent<AudioSource>();
		}

		public void PlaySFX(AudioClip clip)
		{
			if (clip == null)
				return;

			_source.clip = clip;
			_source.Play();
		}
	}
}

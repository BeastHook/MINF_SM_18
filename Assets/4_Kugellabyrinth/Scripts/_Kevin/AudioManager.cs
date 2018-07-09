using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _4_Kugellabyrinth._Kevin
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioManager : MonoBehaviour
	{
		public static AudioManager Instance;
		
		[SerializeField] private float _fadeDuration;
		[SerializeField] private AudioClip[] _ambientMusic;

		private AudioSource _source;
		private float _targetVolume;
		private int _currentLevel;

		private void OnEnable()
		{
			DefaultTrackableEventHandler.OnTrackingEvent += Fade;
		}

		private void OnDisable()
		{
			DefaultTrackableEventHandler.OnTrackingEvent -= Fade;
		}

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

			_currentLevel = 0;

			_source = GetComponent<AudioSource>();
			_source.clip = _ambientMusic[_currentLevel];
			_source.loop = true;

			_targetVolume = _source.volume;
		}

		public void Fade(bool fadeIn)
		{
			if (fadeIn)
			{
				_source.Play();
				_source.DOFade(_targetVolume, _fadeDuration);
			}
			else
			{
				_source.DOFade(0, _fadeDuration).OnComplete(() => _source.Stop());
			}
		}

		public IEnumerator NextLevel()
		{
			_currentLevel++;
			Fade(false);
			yield return new WaitForSeconds(_fadeDuration);
			_source.clip = _ambientMusic[_currentLevel];
			Fade(true);
		}
	}
}

using System;
using System.Collections;
using Objects;
using UnityEngine;
using Player;

namespace Traps
{
	public class Trap : VuMono
	{
		private enum AnimationState
		{
			FirstActivation,
			Idle,
			Active,
			Static
		}

		[SerializeField, Tooltip("If this is != null, it is used other than the standard HitSound in SFXManager")] private AudioClip _customHitSound;
		[SerializeField, Tooltip("If its set to 0, the animation will loop without a pause")] private float _waitTime;
		[SerializeField] private float _startAfter;
		[SerializeField] private bool _isAnimatedTrap = true;

		private AnimationState _animState;
		private Animation _animation;
		private bool _didAnimate;
		protected bool _canDamage;
		private float _currentWaitTime;

		protected override void Awake()
		{
			base.Awake();

			_animation = GetComponent<Animation>();
			if(_animation)
				_animation.playAutomatically = false;

			_animState = _isAnimatedTrap ? AnimationState.Idle : AnimationState.Static;
			_canDamage = !_isAnimatedTrap;
		}

		private void OnEnable()
		{
			if(_animState == AnimationState.FirstActivation)
			{
				StartCoroutine(DelayTrapStart());
			}
		}

		private void Update()
		{
			if (!gameObject.activeSelf) return;

			switch (_animState)
			{
				case AnimationState.Idle:
					_currentWaitTime += Time.deltaTime;
					if (_currentWaitTime > _waitTime)
					{
						_animState = AnimationState.Active;
						_currentWaitTime = 0;
					}
					break;
				case AnimationState.Active:
					if (!_animation.isPlaying && _didAnimate && _waitTime > 0f)
					{
						_didAnimate = false;
						_animState = AnimationState.Idle;
						return;
					}

					if (!_animation.isPlaying && (!_didAnimate || _waitTime == 0f))
					{
						StartTrap();
					}

					break;
				case AnimationState.Static:
					break;
				default:
					throw new ArgumentOutOfRangeException("ArgumentOutOfRange in Trap, invalid animState!");
			}

		}

		protected virtual void StartTrap()
		{
			_animation.Play();
			_canDamage = true;
			_didAnimate = true;
		}

		protected virtual void DisableDamage()
		{
			_canDamage = false;
		}

		private IEnumerator DelayTrapStart()
		{
			yield return new WaitForSeconds(_startAfter);
			_animState = AnimationState.Idle;
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
			if (!_canDamage || !other.CompareTag("Player")) return;

			SFXManager.Instance.PlaySFX(_customHitSound != null ? _customHitSound : SFXManager.Instance.HitSound);
			Debug.Log("Respawning Player, Context: " + gameObject.name + ", parent obj: " + transform.root.name);
			SpawnManager.Instance.Respawn();
		}
	}
}

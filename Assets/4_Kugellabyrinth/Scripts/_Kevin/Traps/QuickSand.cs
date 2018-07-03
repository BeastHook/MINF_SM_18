using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Traps
{
	public class QuickSand : VuMono
	{
		[Header("Other Quicksand")]
		[SerializeField] private QuickSand _exit;

		[Header("Spit Setup")]
		[SerializeField, Range(0.1f, 1.0f)] private float _waitTimeUntilSpit = .5f;
		[SerializeField, Tooltip("Minimum height that this trap can spit the player out")] private float _minSpitHeight;
		[SerializeField, Tooltip("Maximum height that this trap can spit the player out")] private float _maxSpitHeight;
		[SerializeField, Tooltip("Maximum Flight Duration")] private float _maxFlightDuration;
		[SerializeField, Tooltip("AnimationCurve used for easing the spitHeight")] private AnimationCurve _spitEasingCurve;
		[SerializeField, FormerlySerializedAs("_dustParticleSystem")] private ParticleSystem _spitParticleSystem;

		[Header("Drag Setup")]
		[SerializeField, Tooltip("Time it takes to drag the player into the middle")] private float _dragInDuration;

		private Transform _landingPoint;
		private bool _canDrag = true;

		protected override void Awake()
		{
			base.Awake();

			if (_maxSpitHeight < _minSpitHeight)
				_maxSpitHeight = _minSpitHeight;

			_landingPoint = GetComponentsInChildren<Transform>()[1];
		}

		private void DragPlayerIn(PlayerController player)
		{
			SFXManager.Instance.PlaySFX(SFXManager.Instance.QuickSandDragSound);
			_canDrag = false;
			player.transform.DOMove(transform.position, _dragInDuration).SetEase(AnimationCurve.Linear(0, 0, 1, 1)).OnComplete(
				() =>
				{
					player.Toggle(false);
					StartCoroutine(_exit.SpitPlayerOut(player));
				});
		}

		public IEnumerator SpitPlayerOut(PlayerController player)
		{
			yield return new WaitForSeconds(_waitTimeUntilSpit);
			_spitParticleSystem.Play();
			player.gameObject.SetActive(true);
			SFXManager.Instance.PlaySFX(SFXManager.Instance.QuickSandSpitSound);

			player.transform.position = transform.position;
			float spitHeight = Random.Range(_minSpitHeight, _maxSpitHeight);
			float duration = spitHeight/_maxSpitHeight * _maxFlightDuration;
			player.transform.DOMoveZ(spitHeight, duration).SetEase(_spitEasingCurve).OnStart(() =>
			{
				player.transform.DOMoveX(_landingPoint.position.x, duration);
				player.transform.DOMoveY(_landingPoint.position.y, duration);
			}).OnComplete(() =>
			{
				player.Toggle(true);
				StartCoroutine(DragCooldown());
			});
		}

		private IEnumerator DragCooldown()
		{
			yield return new WaitForSeconds(1.5f);
			_canDrag = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player") && _canDrag)
			{
				DragPlayerIn(other.GetComponent<PlayerController>());
			}
		}
	}
}

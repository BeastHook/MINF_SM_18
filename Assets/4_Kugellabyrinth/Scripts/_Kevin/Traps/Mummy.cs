using System;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
using _4_Kugellabyrinth._Kevin.Player;
using Random = UnityEngine.Random;

namespace _4_Kugellabyrinth._Kevin.Traps
{
	public class Mummy : VuMono
	{
		private enum MummyBrain
		{
			Idle,
			RandomWalk,
			ChasePlayer
		}

		[SerializeField] private Player.PlayerController _player;
		[SerializeField] private float _aggroRange;
		[SerializeField] private Transform[] _waypoints;
		[SerializeField, Tooltip("If you want a mummy that patrols only between 2 points, tick this and assign no more than 2 waypoints")] private bool _isOnlyPatrol;
		private int _currentWaypointIndex;

		private MummyBrain _brain = MummyBrain.Idle;

		private Animator _animator;
		private NavMeshAgent _agent;
		private bool _isActive;

		protected override void Awake()
		{
			base.Awake();
			_animator = GetComponent<Animator>();
			_agent = GetComponent<NavMeshAgent>();

			_brain = MummyBrain.RandomWalk;
			_currentWaypointIndex = 0;
		}

		protected override void OnTracking(bool trackingfound, TrackableBehaviour trackable)
		{
			base.OnTracking(trackingfound, trackable);

			_isActive = trackingfound;
		}

		private void Update()
		{
			switch (_brain)
			{
				case MummyBrain.Idle:
					break;
				case MummyBrain.RandomWalk:
					if (Vector3.Distance(transform.position, _agent.destination) < 0.1f)
					{
						_agent.SetDestination(GetNextWaypoint(true).position);
					}
					break;
				case MummyBrain.ChasePlayer:
					break;
				default:
					throw new ArgumentOutOfRangeException("Mummy " + this + " is in invalid State while update");
			}

			GetNextState();
		}

		private Transform GetNextWaypoint(bool chooseRandomWaypoint)
		{
			if (!chooseRandomWaypoint) return _waypoints[++_currentWaypointIndex];

			int randomIndex = 0;
			do
			{
				randomIndex = Random.Range(0, _waypoints.Length);
			} while (randomIndex == _currentWaypointIndex);

			_currentWaypointIndex = randomIndex;
			return _waypoints[randomIndex];
		}

		private void GetNextState()
		{
			switch (_brain)
			{
				case MummyBrain.Idle:
					if (_isActive)
						_brain = MummyBrain.RandomWalk;
					break;
				case MummyBrain.RandomWalk:
					if (PlayerInRange() && _player.IsChasable && !_isOnlyPatrol)
						_brain = MummyBrain.ChasePlayer;
					break;
				case MummyBrain.ChasePlayer:
					if (!PlayerInRange() || !_player.IsChasable)
						_brain = MummyBrain.RandomWalk;
					break;
				default:
					throw new ArgumentOutOfRangeException("State Transition failed" + this);
			}

			OnStateChanged(_brain);
		}

		private bool PlayerInRange()
		{
			return Vector3.Distance(transform.position, _player.transform.position) < _aggroRange;
		}

		private void OnStateChanged(MummyBrain newState)
		{
			switch (newState)
			{
				case MummyBrain.Idle:
					_animator.SetBool("isRun", false);
					_animator.SetBool("crippled", false);
					break;
				case MummyBrain.RandomWalk:
					_animator.SetBool("isRun", true);
					_animator.SetBool("crippled", false);
					break;
				case MummyBrain.ChasePlayer:
					_animator.SetBool("isRun", false);
					_animator.SetBool("crippled", true);
					_agent.SetDestination(_player.transform.position);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				SpawnManager.Instance.Respawn();
			}
		}
	}
}

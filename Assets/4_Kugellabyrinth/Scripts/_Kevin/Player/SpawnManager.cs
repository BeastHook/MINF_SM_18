using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Player
{
	public class SpawnManager : MonoBehaviour
	{
		public static SpawnManager Instance;

		public delegate void RespawnAction(Transform spawnPoint);
		public static event RespawnAction OnRespawn;

		[Tooltip("If enabled the player will always be reset to StartPosition")]
		[SerializeField] private bool _hardResetSpawns;
		[SerializeField] private SpawnPoint[] _startPositions;

		private SpawnPoint _currentSpawnPoint;
		private int _level;

		public int Level
		{
			get { return _level; }
			set { _level = value; }
		}

		private void Awake()
		{
			if (Instance != this && Instance != null)
				Destroy(gameObject);
			else
				Instance = this;

			_level = 0;
			_currentSpawnPoint = _startPositions[_level];
		}
		
		public void Respawn(bool atLevelStart = false)
		{
			OnRespawn?.Invoke(atLevelStart || _hardResetSpawns ? _startPositions[_level].CachedTransform : _currentSpawnPoint.CachedTransform);
		}

		public void UpdateSpawnpoint(SpawnPoint newSpawn)
		{
			_currentSpawnPoint = newSpawn;
		}

		public void SwapLevel(int level)
		{
			_level = level;
			_currentSpawnPoint = _startPositions[level];
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _4_Kugellabyrinth._Kevin
{
	public class NavMeshBaker : VuMono
	{
		[SerializeField] [Range(0f, 10f)] private float _navMeshUpdateInterval = 1.0f;
		[SerializeField] private List<NavMeshSurface> _navMeshSurfaces;

		private float _waitTime;

		private void Update()
		{
			_waitTime += Time.deltaTime;

			if (_waitTime < _navMeshUpdateInterval) return;

			foreach (NavMeshSurface surface in _navMeshSurfaces)
			{
				surface.BuildNavMesh();
			}

			_waitTime = 0f;
		}
	}
}

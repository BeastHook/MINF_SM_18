using UnityEngine;

namespace Player
{
	public class SpawnPoint : VuMono
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				SpawnManager.Instance.UpdateSpawnpoint(this);
			}
		}
	}
}

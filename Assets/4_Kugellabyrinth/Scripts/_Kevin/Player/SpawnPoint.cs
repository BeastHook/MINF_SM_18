using UnityEngine;

namespace _4_Kugellabyrinth._Kevin.Player
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

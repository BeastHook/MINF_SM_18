using UnityEngine;

namespace _4_Kugellabyrinth._Kevin
{
	public class FinishingArea : MonoBehaviour {

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				GameManager.Instance.LevelDone();
			}
		}
	}
}

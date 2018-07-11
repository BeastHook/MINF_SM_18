using UnityEngine;

//Ruft im Hindernis eine Funktion auf - sendet den Namen mit
namespace _7_TinyToolTime.Scripts
{
	public class Tool : MonoBehaviour {

		public int intTool;


		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Help"))
			{
				other.GetComponent<HelpWindow>().ContactWithTool(intTool);
			}
		}

	}
}

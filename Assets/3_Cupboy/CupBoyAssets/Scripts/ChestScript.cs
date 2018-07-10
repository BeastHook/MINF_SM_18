using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour {
	//public Text winText;
	public GameObject letter;
	public GameObject game;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals("CupBoy"))
		{
			collision.GetComponent<AutomatedMovement>().moveallowed = false;
			this.GetComponent<Animator>().SetBool("isOpen", true);
			// winText.GetComponent<Text>().enabled = true; 
			//winText.text = ("Winner!"+"\n"+"A");
			letter.SetActive(true);
			game.SetActive(false);
			//End Game
			MultisceneManager.Instance.StartCoroutine(MultisceneManager.Instance.FinishLevel(true));
		}
	}
}

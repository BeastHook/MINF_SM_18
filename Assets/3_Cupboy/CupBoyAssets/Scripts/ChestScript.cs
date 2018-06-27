using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour {

    public Text winText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<AutomatedMovement>().moveallowed = false;
        this.GetComponent<Animator>().SetBool("isOpen" ,true);
        // winText.GetComponent<Text>().enabled = true; 
        winText.text = ("Winner!"+"\n"+"A");
    }
}

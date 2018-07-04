using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTipAudio : MonoBehaviour {

   public AudioSource audioS;
    private bool alreadyPlayed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("CupBoy")&&!alreadyPlayed) {
            audioS.Play();
            alreadyPlayed = true;
        }
    }
}

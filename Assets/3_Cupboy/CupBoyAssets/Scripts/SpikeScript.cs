using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    void Start () {
		
	}
	
	
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("CupBoy"))
        {
            player.GetComponent<AutomatedMovement>().SetToCheckpoint();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upplatfromCollision : MonoBehaviour {
    public GameObject platform;
    public GameObject player;
	// Use this for initialization
	void Start () {
       


	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        platform.GetComponent<Upplatfrom>().move = true;
        player.GetComponent<AutomatedMovement>().moveallowed = false;
    }
}

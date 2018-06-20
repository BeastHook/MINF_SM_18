using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField]
    private int number = 0;
    [SerializeField]
    private GameObject player;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("CupBoy"))
        {
            player.GetComponent<AutomatedMovement>().Stage = number;
        }
    }
}

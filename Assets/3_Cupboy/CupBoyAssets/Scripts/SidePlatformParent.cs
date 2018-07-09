using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlatformParent : MonoBehaviour {

    public Transform platform;
    public Transform imagetarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("CupBoy")) {
            collision.collider.transform.SetParent(platform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("CupBoy")) {
            collision.collider.transform.SetParent(imagetarget);
        }
    }
    
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public MyPlayerController player;
    //public MyPlayerController player;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
       
        Vector3 direction = new Vector3(player.moveSpeed,0, 0);
        this.transform.Translate(direction*Time.deltaTime);
       
	}
}

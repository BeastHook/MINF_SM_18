using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int health = 100;
    
    public int playerState = 0;
    public AudioSource source;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        
	}

    public void getDamaged(int damage)
    {
        health = health - damage;
    }

    public void getHealed(int damage)
    {
        health = health + damage;
    }
}

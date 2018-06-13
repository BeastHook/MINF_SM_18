using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int health = 100;
    public Slider playerHealth;
    public int playerState = 0;

	// Use this for initialization
	void Start () {
        playerHealth.maxValue = 100;
    }
	
	// Update is called once per frame
	void Update () {

        playerHealth.value = health;
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

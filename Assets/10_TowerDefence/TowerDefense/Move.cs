﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Move : MonoBehaviour {
    Animator anim;
    float speed = 0.1f;
    float turnSpeed = 1.0f;
    public bool dead = false;

    public GameObject gameCounter;

    private GameCounter gm;
   
    
    //private float count;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "bullet")
        {
            Hit();
            Destroy(col.gameObject); //.SetActive(false);
            gm.count++;
            
        }
        else if(col.gameObject.tag == "home")
        {
            Destroy(this.gameObject,1);
            this.GetComponent<AudioSource>().Play();
        }
    }

    public void Hit()
    {
        dead = true;
        anim.SetTrigger("IsDying");
        Destroy(this.GetComponent<Collider>(),1.0f);
        Destroy(this.GetComponent<Rigidbody>(),1.0f);
        Destroy(this.gameObject,4.0f);
        
    }

    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        gameCounter = GameObject.Find("GameCounter");
        gm = gameCounter.GetComponent<GameCounter>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (dead)
        {
            return;
            
        }

            GameObject home = GameObject.FindWithTag("home");
        if(home != null)
    	{
    		Vector3 direction = home.transform.position - this.transform.position;
        	this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
        						Quaternion.LookRotation(direction), 
        						turnSpeed * Time.smoothDeltaTime);
        }
        
        this.transform.Translate(0, 0, speed);
	}
}

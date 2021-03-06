﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Attack : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject vbBtnObj;
    public float timer = 0.0f;
    public bool timerstart = false;

    public GameObject Enemy;
    public GameObject Player;
    public AudioSource source;
    public AudioClip feuer;
    public GameObject explo;
    


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("BTN PRESSED");
        timerstart = true;
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("LOST");
        timerstart = false;
        timer = 0.0f;
    }

    // Use this for initialization
    void Start () {
        vbBtnObj = GameObject.Find("VirtualButton");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        
        

    }

     void Update()
    {
        if (timerstart && Enemy.GetComponent<EnemyController>().playerturn == true)
        {
            timer = timer + Time.deltaTime;

        }   

        if(timer >= 2.0f)
        {
            Debug.Log("Attack");
            source.clip = feuer;
            source.Play();
            explo.SetActive(true);
            StartCoroutine(Explosion());
            Enemy.GetComponent<EnemyController>().getDamaged(25);
            Player.GetComponent<AnimControl>().Attack01();
            timerstart = false;
            timer = 0.0f;
            Enemy.GetComponent<EnemyController>().playerturn = false;
            Enemy.GetComponent<EnemyController>().enemyturn = true;
        }

    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2);
        explo.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Frostwall: MonoBehaviour, IVirtualButtonEventHandler
{

    public GameObject vbBtnObj;
    public float timer = 0.0f;
    public bool timerstart = false;

    public GameObject Enemy;
    public GameObject Player;
    public AudioSource source;
    public AudioClip frost;


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
    void Start()
    {
        vbBtnObj = GameObject.Find("VirtualButton2");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

    }

    void Update()
    {
        if (timerstart && Enemy.GetComponent<EnemyController>().playerturn == true)
        {
            timer = timer + Time.deltaTime;

        }

        if (timer >= 2.0f)
        {
            Debug.Log("Frostwall");
            source.clip = frost;
            source.Play();
            Player.GetComponent<PlayerController>().playerState = 1;
            timerstart = false;
            timer = 0.0f;
            Enemy.GetComponent<EnemyController>().playerturn = false;
            Enemy.GetComponent<EnemyController>().enemyturn = true;
        }

    }
}

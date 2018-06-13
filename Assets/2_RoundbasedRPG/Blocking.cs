using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Blocking : MonoBehaviour, IVirtualButtonEventHandler
{

    public GameObject vbBtnObj;
    public float timer = 0.0f;
    public bool timerstart = false;

    public GameObject Enemy;
    public GameObject Player;


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
        vbBtnObj = GameObject.Find("VirtualButton3");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

    }

    void Update()
    {
        if (timerstart && Enemy.GetComponent<EnemyController>().playerturn == true)
        {
            timer = timer + Time.deltaTime;

        }

        if (timer >= 5.0f)
        {
            Debug.Log("Blocking");
            Player.GetComponent<PlayerController>().playerState = 2;
            timerstart = false;
            timer = 0.0f;
            Enemy.GetComponent<EnemyController>().playerturn = false;
            Enemy.GetComponent<EnemyController>().enemyturn = true;
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public int enemyhealth = 100;
    public Slider enemyHealthSlider;
    public bool enemyturn = false;
    public bool playerturn = true;
    public GameObject player;
    public int enemyState = 0;
    public int[] stateList = { 1, 2, 3, 4, 5, 1, 2,3,4,5,1,2,3,4,5,1,2,3,4,5}; 

    // Use this for initialization
    void Start() {
        enemyHealthSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update() {
        enemyHealthSlider.value = enemyhealth;

        if (enemyturn)
        {
            Debug.Log("ENEMY TURN");
            StartCoroutine(EnemyAttack());
            enemyturn = false;

        }
    }

    private IEnumerator EnemyAttack()
    {
        int playerstate = player.GetComponent<PlayerController>().playerState;
        yield return new WaitForSeconds(3);

        if (stateList[enemyState] == 1)
        {
            Debug.Log("Flammenatem");
        }
        else if (stateList[enemyState] == 2)
        {
            if(playerstate != 1)
            {
                player.GetComponent<PlayerController>().getDamaged(50);
                player.GetComponent<PlayerController>().playerState = 0;
            }
            else
            {
                player.GetComponent<PlayerController>().playerState = 0;
            }
        }
        else if (stateList[enemyState] == 3)
        {
            Debug.Log("Ausholen");
        }
        else if (stateList[enemyState] == 4)
        {
            if (playerstate != 2)
            {
                player.GetComponent<PlayerController>().getDamaged(40);
                player.GetComponent<PlayerController>().playerState = 0;
            }
            else
            {
                player.GetComponent<PlayerController>().playerState = 0;
            }
        }
        else if (stateList[enemyState] == 5)
        {
            Debug.Log("Taumeln");
        }
       


        enemyState++;
        playerturn = true;
        Debug.Log("PLAYER TURN");


    }
   

    public void getDamaged(int damage)
    {
        enemyhealth = enemyhealth - damage;
    }

   
    


}

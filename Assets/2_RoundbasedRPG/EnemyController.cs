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

    // Use this for initialization
    void Start() {
        enemyHealthSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update() {
        enemyHealthSlider.value = enemyhealth;

        if (enemyturn)
        {
            StartCoroutine(EnemyAttack());
            enemyturn = false;

        }
    }

    private IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(3);
        player.GetComponent<PlayerController>().getDamaged(20);
        
        playerturn = true;


    }
   

    public void getDamaged(int damage)
    {
        enemyhealth = enemyhealth - damage;
    }

   
    


}

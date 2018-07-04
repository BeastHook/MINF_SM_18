using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public int enemyhealth = 100;
    
    public bool enemyturn = false;
    public bool playerturn = true;
    public GameObject player;
    public int enemyState = 0;
    public int[] stateList = { 1, 2, 3, 4, 5, 1, 2,3,4,5,1,2,3,4,5,1,2,3,4,5};
    public AudioSource source;
    public AudioClip holtluft;
    public AudioClip feuerschild;
    public AudioClip feuertreffer;
    public AudioClip klaue;
    public AudioClip klaueschild;
    public AudioClip klauenicht;
    public AudioClip stolpern;
    public AudioClip gewonnen;
    public AudioClip verloren;
    public Boolean gameEnd = false;
    public GameObject chest;
    public GameObject flame;
    
    



    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        

        if (enemyturn && gameEnd == false )
        {
            Debug.Log("ENEMY TURN");
            if (player.GetComponent<PlayerController>().health <= 0)
            {
                StartCoroutine(Loose());
                gameEnd = true;

            }

            if (enemyhealth > 0)
            {
                StartCoroutine(EnemyAttack());
                enemyturn = false;
            }
            else
            {
                StartCoroutine(Win());
                gameEnd = true;
            }

            

        }
    }

    private IEnumerator Loose()
    {
        yield return new WaitForSeconds(7);
        source.clip = verloren;
        source.Play();
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(7);
        
        source.clip = gewonnen;
        source.Play();
        yield return new WaitForSeconds(3);
        chest.SetActive(true);
    }

    private IEnumerator EnemyAttack()
    {
        int playerstate = player.GetComponent<PlayerController>().playerState;
        yield return new WaitForSeconds(7);

        if (stateList[enemyState] == 1)
        {
            source.clip = holtluft;
            source.Play();
            Debug.Log("Flammenatem");
            this.gameObject.GetComponent<playerControl>().FlameAttack();
            player.GetComponent<PlayerController>().playerState = 0;

        }
        else if (stateList[enemyState] == 2)
        {
            if(playerstate != 1)
            {
                source.clip = feuertreffer;
                source.Play();
                this.gameObject.GetComponent<playerControl>().FlyFlameAttack();
                flame.SetActive(true);
                StartCoroutine(Fire());
                player.GetComponent<PlayerController>().getDamaged(50);
                player.GetComponent<PlayerController>().playerState = 0;
            }
            else
            {
                source.clip = feuerschild;
                source.Play();
                this.gameObject.GetComponent<playerControl>().FlyFlameAttack();
                flame.SetActive(true);
                StartCoroutine(Fire());
                player.GetComponent<PlayerController>().playerState = 0;
            }
        }
        else if (stateList[enemyState] == 3)
        {
            source.clip = klaue;
            source.Play();
            this.gameObject.GetComponent<playerControl>().BasicAttack();
            Debug.Log("Ausholen");
            player.GetComponent<PlayerController>().playerState = 0;
        }
        else if (stateList[enemyState] == 4)
        {
            if (playerstate != 2)
            {
                source.clip = klauenicht;
                source.Play();
                this.gameObject.GetComponent<playerControl>().ClawAttack();
                player.GetComponent<PlayerController>().getDamaged(40);
                player.GetComponent<PlayerController>().playerState = 0;
            }
            else
            {
                source.clip = klaueschild;
                source.Play();
                this.gameObject.GetComponent<playerControl>().ClawAttack();
                player.GetComponent<PlayerController>().playerState = 0;
            }
        }
        else if (stateList[enemyState] == 5)
        {
            source.clip = stolpern;
            source.Play();
            this.gameObject.GetComponent<playerControl>().GetHit();
            Debug.Log("Taumeln");
            player.GetComponent<PlayerController>().playerState = 0;
        }
       


        enemyState++;
        playerturn = true;
        Debug.Log("PLAYER TURN");


    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(3);
        flame.SetActive(false);

    }

    public void getDamaged(int damage)
    {
        enemyhealth = enemyhealth - damage;
    }

   
    


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTiny : MonoBehaviour {

    public bool canWalk = false;
    public float speed = 0.2f;

    private PlayerAnimation anim;
    public bool rotate = false;
    private float speedReduction;

    private float rotationAngle;
    public GameObject switchPlayer;
    public GameObject switchPlayer2;

    private void Start()
    {
        anim = GetComponent<PlayerAnimation>();

        speedReduction = (speed * 0.15f); //Reduce the speed with damage
        rotationAngle = 270;
        
    }   

    // Update is called once per frame
    void Update () {
        if (canWalk)
            PlayerMove();       
    }

    internal void SetCanWalk(bool v) //Stop/start walking, stop/Start walking Animation
    {     
            canWalk = v;
            anim.Walk(v);                 
    }

    void PlayerMove() {

           transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.CompareTag("Obstacle")) //collide the first time?
        {           
            SetCanWalk(false); //Stop when reached the TrapTrigger
            anim.SetObstacle(col.gameObject);
            print("Collider with Obstacle");
        }

        else if (col.gameObject.CompareTag("NextLevel")) //LevelEnd, Change Direction
        {
            FinnishLevel();
        }

        else if (col.gameObject.CompareTag("Finish")) //LevelEnd, Change Direction
        {
            GameOver();
        }
    }

    private void GameOver()
    {              
        canWalk = false;
        anim.GameOver();
        
    }

    public void FinnishLevel() {
        canWalk = false;
        anim.PlayAnimation(11);

    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        rotationAngle -= 180;
        // rotatePlayer.transform.SetPositionAndRotation(newPosPlayer.transform.position, newPosPlayer.transform.rotation); //setPosition Player 2
        gameObject.transform.SetPositionAndRotation(transform.position, switchPlayer.transform.rotation); //soll die Position übernehmen nach der Drehung
        switchPlayer = switchPlayer2;
    }

    public void CanWalk() //###### Called by Event in Victory ###########
    {
        SetCanWalk(true);
    }

    public void RotatePlayer()
    {
        StartCoroutine(RotateMe(Vector3.up * 180, 0.8f)); //Rotate the Player           
    }

    internal void ReduceSpeed()
    {
        speed -= speedReduction;       
    }

 
}

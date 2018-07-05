using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTiny : MonoBehaviour {

    public bool canWalk = false;
    public float speed = 0.2f;

    private PlayerAnimation anim;
    private bool directionRight; //?????????
    public bool rotate = false;

    private Renderer rend;
    private float speedReduction;

    private float rotationAngle;

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
        directionRight = !directionRight; //?????????????
 //       SetCanWalk(false);
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

        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.time * 0.1f);
        //       float rotato = transform.rotation.y + 180;
        //       transform.rotation = Quaternion.Euler(0, rotato, 0);
        //       float rotato = transform.rotation.y + 180;

        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
        rotationAngle -= 180;

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

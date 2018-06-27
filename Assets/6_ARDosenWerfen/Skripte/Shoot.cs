using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject ballPrefab;
    public Transform spawnObject;
    public Animator gunsightAnim;


    public static int ballAmount = 3;
    public static bool canShoot = true;
    public static  float elapsedTime;

    public static bool waiting = false;
    public static bool allowedToShoot;


    public float Power;
    public float maxPower;


    // Use this for initialization
    void Start () {
        gunsightAnim.GetComponent<Animator>();
        allowedToShoot = true;
    }

    void FixedUpdate() {
       
        // Wird alle 3 Sekunden
        if (allowedToShoot == true)
        {

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 3.0)
           {
           
                waiting = true;
                gunsightAnim.SetBool("gunsight", false);
            }  
        }

        if (Input.GetMouseButton(0) && waiting == true && allowedToShoot == true)
        {

                Power += Time.deltaTime * 30;
                Power = Mathf.Clamp(Power, 0, maxPower);       
        }

        if (Input.GetMouseButtonUp(0) && waiting == true && allowedToShoot == true)
        {
          //  FindObjectOfType<AudioManager>().Play("wurf");
            gunsightAnim.SetBool("gunsight", true);
            GameObject ball = Instantiate(ballPrefab, spawnObject.position, spawnObject.rotation) as GameObject;
            ball.GetComponent<Rigidbody>().AddForce(transform.forward * Power, ForceMode.Impulse);
     

            ballAmount--;
            elapsedTime = 0;
            Power = 60f;
            waiting = false;
           
        }


        if (ballAmount == 3)
        {
            canShoot = true;
        }

        if (ballAmount == 0)
        {

           if ( elapsedTime >= 2.5f )
            {
                canShoot = false;
            }
            
        }
    }
}

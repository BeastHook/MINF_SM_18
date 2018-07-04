using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshoot : MonoBehaviour {
    public GameObject projectile;
    public Transform shootpoint;
   public  float timer;
   public  float waitingTime  =1;
    public Transform parent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            shoot();
            timer = 0;
        }
    }

    void shoot()
    {
        //Quaternion rot = Quaternion.Euler(90, 0, 0);

        Instantiate(projectile, shootpoint.position, transform.rotation,parent);


        //Quaternion rot=Quaternion.Euler(0, 0, AngleDeg);
        //Instantiate(projectile, staffTip.position, rot);
    }
}

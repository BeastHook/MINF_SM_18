using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;


public class TowerBehaviour : MonoBehaviour
    { 

    public GameObject bullet;
    
    public GameObject parent;

    private Spawner spawner;
    private Move move;
    public Text score;
    private float count;
    public bool done = false;


    public Transform spawnPos;
	GameObject goob;
   
	
	void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.tag == "goober" && goob == null)
		{
			goob = obj.gameObject;
			InvokeRepeating("Shoot",0,1.0f);
		}
	}

  

    void Shoot()
	{
		var newBullet = Instantiate(bullet, spawnPos.position, spawnPos.rotation);
        newBullet.transform.parent = parent.transform;
        //this.GetComponent<AudioSource>().Play();
        if (goob.GetComponent<Move>().dead)
		{

            if (count == 30)
            {
                done = true;
            }
            else
            {
                count++;
            }
            
            
            


			goob = null;
			CancelInvoke("Shoot");
		}
	}

    

    void OnTriggerExit(Collider obj)
	{
		if(obj.gameObject == goob)
		{
			goob = null;
			CancelInvoke("Shoot");
		}
	}
	// Use this for initialization
	void Start () {

       

    }
	
	// Update is called once per frame
	void Update () 
	{
        if (goob != null)
        {
            this.transform.LookAt(goob.transform.position);
        }

        score.text = "Score: " + count;



    }
}

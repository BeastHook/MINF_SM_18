using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;


public class TowerBehaviour : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public GameObject bullet;
    
    public GameObject canon;

    private Spawner spawner;
    private Move move;
    public TextMesh score;
    public float count;
    public bool done = false;

    private GameObject gameCounter;
    private GameCounter gc;

    public Transform spawnPos;
	GameObject goob;

    void Start()
    {
        gameCounter = GameObject.Find("GameCounter");
        gc = gameCounter.GetComponent<GameCounter>();

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        //tb = turret.GetComponent<TowerBehaviour>();
    }

    void OnTriggerStay(Collider obj)
	{
		if(obj.gameObject.tag == "goober" && goob == null)
		{
		    goob = obj.gameObject;
            Debug.Log("OnTriggerEnter " + obj.gameObject.tag);

        }

    }

    public void OnTrackableStateChanged(
                                   TrackableBehaviour.Status previousStatus,
                                   TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            StartCoroutine("Shoot");
            //Debug.Log("start coroutine");

        }
        else 
        {
            StopCoroutine("Shoot");
        }
    }

    IEnumerator Shoot()
    {
        Debug.Log("shoot ");
        while (!done)
        {
               
            GameObject newBullet = Instantiate(bullet, spawnPos.position-new Vector3(0,0.1f,0), spawnPos.rotation);
            //newBullet.transform.parent = parent.transform;  

            
            yield return new WaitForSeconds(1.0f);
        }
        
    }


    /*void OnTriggerExit(Collider obj)
	{
		if(obj.gameObject == goob)
		{
			goob = null;
            isOnTrigger = false;
            //(CancelInvoke("Shoot");
            //
		}
	}*/

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gc.count);

        if (gc.count >= 30)
        {
            done = true;
            gc.done = true;
        }
        /*Debug.Log(goob.GetComponent<Move>().count);
        if (goob != null)
        {
            if (goob.GetComponent<Move>().count == 30)
            {

                
                

            }
        }*/
    } 
}

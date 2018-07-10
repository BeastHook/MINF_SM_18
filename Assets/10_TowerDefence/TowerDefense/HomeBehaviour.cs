using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class HomeBehaviour : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;

    public GameObject tower;
    public GameObject parent;
    public GameObject flag;
    public GameObject turret;

    private TowerBehaviour tb;
    private bool flagLoaded = false;
    private bool homeLoaded = false;

    // Use this for initialization
    void Start() {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        tb = turret.GetComponent<TowerBehaviour>();
    }

    

    void Spawn()
    {
        if (!homeLoaded)
        {
            var newTower = Instantiate(tower, this.transform.position, this.transform.rotation);
            newTower.transform.parent = parent.transform;
            homeLoaded = true;
        }

    }

    void Flag()
    {
        if (!flagLoaded)
        {
            var newFlag = Instantiate(flag, this.transform.position, this.transform.rotation);
            newFlag.transform.parent = parent.transform;
            flagLoaded = true;
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
            //Start Spawning when target is tracked
            Debug.Log("Spawn Goober");
            Invoke("Spawn", 0f);
        }
        else
        {
            // Stop Spawning when target is lost
            CancelInvoke("Spawn");
        }

        // Update is called once per frame
        
    }

    void Update()
    {
         if (tb.done)
         {
                Debug.Log("geht nicht");
                Invoke("Flag", 0f);
         }
    }
}

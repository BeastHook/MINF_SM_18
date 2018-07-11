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

    private GameObject gameCounter;
    private GameCounter gc;



    // Use this for initialization
    void Start() {

        gameCounter = GameObject.Find("GameCounter");
        gc = gameCounter.GetComponent<GameCounter>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
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
            // not needed
        }
            

        
    }

    void Update()
    {
         if (gc.done)
         {
            flag.SetActive(true);
         }
    }
}

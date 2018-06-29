using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class StartMoving : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public PlayerMovement player;
    bool walkFirstTime = false;

    void Start()
    {
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
            // when target is found
            //           Debug.Log("Tracked");
            if (!walkFirstTime)
            {
                player.SetCanWalk(true);
                walkFirstTime = true;
            }
            
        }
        else
        {
            // when target is lost
//            Debug.Log("Not tracked");
        }
    }

}


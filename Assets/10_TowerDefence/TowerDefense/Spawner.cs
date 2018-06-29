using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Spawner : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    public GameObject goob;
    public GameObject parent;
    private Move move;
    public GameObject turret;


    private TowerBehaviour tb;

    // Use this for initialization
    void Start () {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        tb = turret.GetComponent<TowerBehaviour>();
    }
	

    


	void Spawn()
	{
		var newGoober = Instantiate(goob,this.transform.position, this.transform.rotation);
        newGoober.transform.parent = parent.transform;

    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {



            if (tb.done)
            {

                CancelInvoke("Spawn");
            }
            else
            {
                InvokeRepeating("Spawn", 0f, 0.7f);
                
            }

            //Start Spawning when target is tracked



        }
        else
        {
            // Stop Spawning when target is lost
            CancelInvoke("Spawn");
        }
    }
}

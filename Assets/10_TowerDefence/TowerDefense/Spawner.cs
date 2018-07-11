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
    private GameObject gameCounter;
    private GameCounter gc;


    // Use this for initialization
    void Start()
    {
        gameCounter = GameObject.Find("GameCounter");
        gc = gameCounter.GetComponent<GameCounter>();

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        tb = turret.GetComponent<TowerBehaviour>();
    }

    IEnumerator Spawn()
    {

        while (!gc.done)
        {
           GameObject newGoober = Instantiate(goob, this.transform.position, this.transform.rotation);
           newGoober.transform.parent = this.parent.transform;
           yield return new WaitForSeconds(0.3f);
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

            StartCoroutine("Spawn");

        }

        else
        {
            StopCoroutine("Spawn");
        }
    }
}


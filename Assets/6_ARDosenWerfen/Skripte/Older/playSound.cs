using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class playSound : MonoBehaviour,
ITrackableEventHandler
{
    public AudioSource audioSource;
    private AudioLowPassFilter alpf;
    private TrackableBehaviour mTrackableBehaviour;


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
            // Play audio when target is found
            audioSource.Play();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
            newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            if (audioSource.isPlaying)
            {
                // Stop audio when target is lost
                audioSource.Stop();
            }
        }
    }
}
using UnityEngine;
using System.Collections;
using Vuforia;

public class Detect : MonoBehaviour, ITrackableEventHandler
{

    public TrackableBehaviour mTrackableBehaviour;

    private bool mShowGUIButton = false;
    private Rect mButtonRect = new Rect(50, 50, 120, 60);
    private GameObject gunsight;
    void Start()
    {
        gunsight  = GameObject.FindGameObjectWithTag("gunsight");
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
        newStatus == TrackableBehaviour.Status.TRACKED)
        {
            // SHOW
            GameplayManager.show = true;
            GameplayManager.hide = false;
            gunsight.SetActive(true);

        }
        else
        {
            // HIDE
            GameplayManager.show = false;
            GameplayManager.hide = true;
            gunsight.SetActive(false);



        }
    }

    void OnGUI()
    {
        if (mShowGUIButton)
        {
            // draw the GUI button
            if (GUI.Button(mButtonRect, "Hello"))
            {
                // do something on button click 
            }
        }
    }
}
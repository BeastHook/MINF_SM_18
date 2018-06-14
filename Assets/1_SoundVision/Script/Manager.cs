using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject channel;
    private bool changed;

	// Use this for initialization
	void Start () {
        channel.GetComponent<MicrophoneOn>().enabled = false;
        channel.GetComponent<AudioReader>().enabled = true;
        changed = channel.GetComponent<MicrophoneOn>().isActiveAndEnabled;

    }
    IEnumerator ReenableAudioReader()
    {
        channel.GetComponent<AudioReader>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        channel.GetComponent<AudioReader>().enabled = true;
    }
	
    public void ChangeBetweenMicAndChannel()
    {
        if (!changed)
        {
            channel.GetComponent<MicrophoneOn>().enabled = true;
            changed = true;
        }
        else
        {
            channel.GetComponent<MicrophoneOn>().enabled = false;
            StartCoroutine(ReenableAudioReader());
            changed = false;
        }
    }
}

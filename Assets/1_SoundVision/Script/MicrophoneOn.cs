using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneOn : MonoBehaviour
{

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 22050);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("start playing... position is " + Microphone.GetPosition(null));
        audio.Play();
    }
}


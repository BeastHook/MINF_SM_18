﻿using UnityEngine;
using System.Collections;
using LibPDBinding;
using System;

public class fromPdScript : MonoBehaviour {

	public float fromPd;

	void Start() {

#if UNITY_ANDROID && !UNITY_EDITOR
		Screen.orientation = ScreenOrientation.Landscape;
#endif

		// subscribing to receive
		LibPD.Subscribe("correct");
        LibPD.Float += receiveFloat;
    }

	void OnGUI() {
		GUI.Label(new Rect(10, 50, 300, 300), "correct notes: " + fromPd + "%");
	}

    void receiveFloat(string nameofSend, float value)
    {
        if (String.Compare(nameofSend, "correct") == 0)
        {
            fromPd = value;
        }
    }
}

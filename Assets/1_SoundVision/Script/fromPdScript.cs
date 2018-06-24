using UnityEngine;
using System.Collections;
using LibPDBinding;
using System;

public class fromPdScript : MonoBehaviour {

	public float correct;
	public float debug;

	void Start() {

#if UNITY_ANDROID && !UNITY_EDITOR
		Screen.orientation = ScreenOrientation.Landscape;
#endif

		// subscribing to receive
		LibPD.Subscribe("correct");
        LibPD.Float += receiveFloat;

		LibPD.Subscribe("debug");
		LibPD.Float += receiveFloat;
    }

	void OnGUI() {
		GUI.Label(new Rect(10, 50, 300, 300), "correct notes: " + correct + "%");
		GUI.Label(new Rect(20, 100, 400, 400), "debug: " + debug);
	}

    void receiveFloat(string nameofSend, float value)
    {
        if (String.Compare(nameofSend, "correct") == 0)
        {
            correct = value;
        }
		if (String.Compare(nameofSend, "debug") == 0)
		{
			debug = value;
		}
    }
}

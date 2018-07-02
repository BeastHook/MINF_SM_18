using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    public static bool debug = false;

    public bool setDebugTo = false;

	// Use this for initialization
	void Start () {
        debug = setDebugTo;
	}

}

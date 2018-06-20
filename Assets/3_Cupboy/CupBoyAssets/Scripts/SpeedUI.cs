using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour {
    private float platformSpeed;
    public Text textfield;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.transform.position;
        float distance = pos.magnitude;
       // Debug.Log(distance);
        platformSpeed = distance ;
        textfield.text=("Speed:" +platformSpeed);
    }
}

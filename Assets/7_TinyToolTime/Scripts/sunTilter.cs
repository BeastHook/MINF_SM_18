using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunTilter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.rotation.eulerAngles.z >= 360)
        {
            transform.SetPositionAndRotation(transform.position, transform.rotation = Quaternion.Euler(0, 0, 0));
        }
        transform.Rotate(0, 0, 1);
	}
}

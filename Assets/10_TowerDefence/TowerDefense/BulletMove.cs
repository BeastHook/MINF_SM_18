using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    void OnBecameInvisible()
    {
        DestroyImmediate(this.gameObject);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(-1.0f,0,0);
	}
}

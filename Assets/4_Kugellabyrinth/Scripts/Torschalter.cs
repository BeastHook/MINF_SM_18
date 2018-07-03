using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torschalter : MonoBehaviour {

    public Animator ani;

	// Use this for initialization
	void Start () {
        ani.enabled = false;
	}
	

    private void OnTriggerEnter(Collider other)
    {
        ani.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brückentrigger : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {

        anim.enabled = false;
	}
    

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Do SOmething");
        anim.enabled = true;
    }
}

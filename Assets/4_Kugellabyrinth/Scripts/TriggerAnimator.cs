using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimator : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {

        anim.enabled = false;
	}

    void OnTriggerEnter(Collider other)
    {
        anim.enabled = true;
    }
}

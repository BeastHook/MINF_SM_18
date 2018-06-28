using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = false;
	}
	
    public void StartAnimation()
    {
        anim.enabled = true;
    }
}

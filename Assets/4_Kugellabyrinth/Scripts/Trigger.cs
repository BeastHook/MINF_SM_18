using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public Material[] material;
    public Animator ani;
    Renderer rend;





    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        ani.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + "has collided with " + collision.gameObject.name);
        rend.sharedMaterial = material[1];
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + "has triggered " + other.gameObject.name);
        rend.sharedMaterial = material[1];
        ani.enabled = true;

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}

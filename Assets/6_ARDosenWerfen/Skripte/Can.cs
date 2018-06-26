using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour {

    private Vector3 startPosition;
    private float lastCheck;
    private bool hasFallen;

	void Start () {
        startPosition = transform.position;

        FloatingTextController.Initialize();
	}
	
	// Update is called once per frame
	void Update () {

        if (hasFallen)
            return;

     
        // Bedingung für das Erkennen 
        if (gameObject.transform.rotation.eulerAngles.z < 300)
            {
              

             //   FloatingTextController.CreateFloatingText("10", transform);

            }

             if(Vector3.Magnitude(transform.position - startPosition) > 0.35f)   {
            hasFallen = true;
            GameplayManager.Instance.RemoveCans(this.gameObject);
        }



    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground") ) // More efficient
        {
            if (Vector3.Dot(transform.up, other.transform.up) < 0.9f )
            {
                hasFallen = true;
                GameplayManager.Instance.RemoveCans(this.gameObject);
            }
        }
    }




   

}

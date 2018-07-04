using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger6 : MonoBehaviour
{


    

   // public Transform Group4;
    public GameObject Kopf;
    public GameObject Mitte;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    public GameObject Plane3;
    public GameObject Plane5;

    void Start()
    {
       source=gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("oben5")&&(!played))
        {
            Debug.Log("True");

            source.PlayOneShot(audioC);
            played = true;

            //  Mitte.transform.SetParent(Group4, false);
            Mitte.transform.parent = Kopf.transform;
            Plane5.transform.parent = Plane3.transform;
         //   Kopf.gameObject.tag = "loesung";
            Mitte.gameObject.tag = "loesung";
        
        }
    }

}


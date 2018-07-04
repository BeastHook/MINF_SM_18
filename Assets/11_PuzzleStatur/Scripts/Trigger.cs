using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

   
   // public Transform Group4;
    public GameObject Mitte;
    public GameObject Pferdehinterbeine;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    public GameObject Plane4;
    public GameObject Plane5;

    void Start()
    {source=gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rechts4")&&(!played))
        {
            Debug.Log("True");
         
            source.PlayOneShot(audioC);
            played = true;

            // Mitte.transform.SetParent(Group4, false);
            //Mitte.transform.parent = Group4;
            Pferdehinterbeine.transform.parent = Mitte.transform;
            Plane4.transform.parent = Plane5.transform;
            Mitte.gameObject.tag = "loesung";
            Pferdehinterbeine.gameObject.tag = "loesung";
          
        }
    }

}

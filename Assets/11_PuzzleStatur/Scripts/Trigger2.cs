using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour
{
 

    
    public GameObject Vorderbeine;
    public GameObject Mitte;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;


    public GameObject Plane5;
    public GameObject Plane6;

    void Start()
    {
      source=gameObject.GetComponent<AudioSource> ();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rechts5")&&(!played))
        {
            Debug.Log("True");
           
            source.PlayOneShot(audioC);
            played = true;

            Mitte.transform.parent = Vorderbeine.transform;
            Plane5.transform.parent = Plane6.transform;
            Mitte.gameObject.tag = "loesung";
            Vorderbeine.gameObject.tag = "loesung";
        
        }
    }

}

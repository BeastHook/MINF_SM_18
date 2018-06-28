using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger5 : MonoBehaviour

{
   

    //public Transform Group4;
    public GameObject KörperFlügel;
    public GameObject Pferdehinterbeine;

    public GameObject Plane2;
    public GameObject Plane4;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;


    void Start()
    {
        source= gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("oben4")&& (!played))
        {
            Debug.Log("True");
        
            source.PlayOneShot(audioC);
            played = true;

            //  Mitte.transform.SetParent(Group4, false);
            KörperFlügel.transform.parent = Pferdehinterbeine.transform;
            Plane2.transform.parent = Plane4.transform;
            Pferdehinterbeine.gameObject.tag = "loesung";
            KörperFlügel.gameObject.tag = "loesung";
         
        }
    }

}


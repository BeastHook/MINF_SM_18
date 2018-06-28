using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger7 : MonoBehaviour {



   // public Transform Group4;
    public GameObject Pferdekopf;
    public GameObject Vorderbeine;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    public GameObject Plane1;
    public GameObject Plane6;
    void Start()
    {
        gameObject.GetComponent<AudioSource>();
    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("oben6")&& (!played))
        {
            Debug.Log("True");
            source.PlayOneShot(audioC);
            played = true;
            
            //  Mitte.transform.SetParent(Group4, false);
            Vorderbeine.transform.parent = Pferdekopf.transform;
            Plane6.transform.parent = Plane1.transform;
            Pferdekopf.gameObject.tag = "loesung";
            Vorderbeine.gameObject.tag = "loesung";
          //  source7.Play();
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger3 : MonoBehaviour {
  //  public Transform Group2;
    public GameObject Kopf;
    public GameObject KörperFlügel;
    
    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    public GameObject Plane3;
    public GameObject Plane2;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rechts2")&&(!played))
        {
            Debug.Log("True");

            source.PlayOneShot(audioC);
            played = true;

            //  Mitte.transform.SetParent(Group4, false);
            KörperFlügel.transform.parent = Kopf.transform;
            Plane2.transform.parent = Plane3.transform;
            Kopf.gameObject.tag = "loesung";
            KörperFlügel.gameObject.tag = "loesung";
          
        }
    }
}

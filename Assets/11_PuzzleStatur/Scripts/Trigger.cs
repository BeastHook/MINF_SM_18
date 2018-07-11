using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public GameObject Mitte;
    public GameObject Pferdehinterbeine;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    public GameObject Plane4;
    public GameObject Plane5;

    
    void Start()
    {
        source =gameObject.GetComponent<AudioSource>();
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rechts4")&&(!played))
        {
            playMusic();
            parentingObjects();
            Position();

            //Set LocalPositions in new Parents
            Vector3 test2 = new Vector3(-0.0003881835f, -3.875717e-05f, 0);
            Pferdehinterbeine.transform.localPosition = test2;
            Pferdehinterbeine.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Vector3 test3 = new Vector3(-10.07001f, 0, 0.02999878f);
            Plane4.transform.localPosition = test3;
            Plane4.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Position()
    {
        Vector3 trans2 = Vector3.zero;
        Pferdehinterbeine.transform.localPosition = trans2;
        Pferdehinterbeine.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Vector3 trans3 = Vector3.zero;
        Plane4.transform.localPosition = trans3;
        Plane4.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }
    void playMusic()
    {
        source.PlayOneShot(audioC);
        played = true;
    }
    void parentingObjects()
    {
        Pferdehinterbeine.transform.parent = Mitte.transform;
        Plane4.transform.parent = Plane5.transform;
        Mitte.gameObject.tag = "passt";
        Pferdehinterbeine.gameObject.tag = "passt";
    }
}

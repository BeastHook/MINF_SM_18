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
            playMusic();
            parentingObjects();
            Position();

            //Set LocalPositions in new Parents
            Vector3 test2 = new Vector3(0.04469503f, 0.01622948f, 0.1015999f);
            Mitte.transform.localPosition = test2;
            Mitte.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Vector3 test3 = new Vector3(-10.03999f, 0, -0.009994507f);
            Plane5.transform.localPosition = test3;
            Plane5.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Position()
    {
      
        Vector3 trans2 = Vector3.zero;
        Mitte.transform.localPosition = trans2;
        Mitte.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Vector3 trans3 = Vector3.zero;
        Plane5.transform.localPosition = trans3;
        Plane5.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
    }
    void playMusic()
    {
        source.PlayOneShot(audioC);
        played = true;
    }
    void parentingObjects()
    {
        Mitte.transform.parent = Vorderbeine.transform;
        Plane5.transform.parent = Plane6.transform;
        Mitte.gameObject.tag = "passt";
        Vorderbeine.gameObject.tag = "passt";
    }
    }

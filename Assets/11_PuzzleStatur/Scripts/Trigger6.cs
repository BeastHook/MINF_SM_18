using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger6 : MonoBehaviour
{
 
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
            playMusic();
            parentingObjects();
            Position();

            //Set LocalPositions in new Parents
            Vector3 test2 = new Vector3(-0.0006037506f, -0.001650604f, -3.024578e-05f);
            Mitte.transform.localPosition = test2;
            Mitte.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Vector3 test3 = new Vector3(-0.009994507f, 0, -10.03f);
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

        Mitte.transform.parent = Kopf.transform;
        Plane5.transform.parent = Plane3.transform;
        Kopf.gameObject.tag = "passt2";
        Mitte.gameObject.tag = "passt";
        
    }
    }


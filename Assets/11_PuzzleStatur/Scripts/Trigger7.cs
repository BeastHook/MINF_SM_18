using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger7 : MonoBehaviour {



   // public Transform Group4;
    public GameObject Pferdekopf;
    public GameObject Vorderbeine;
    public GameObject Target1;

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
            playMusic();
            parentingObjects();
            Position();

            //Set LocalPositions in new Parents
            Vector3 test = new Vector3(-0.02970007f, -0.02017949f, -0.101633f);
             Vorderbeine.transform.localPosition = test;
             Vorderbeine.transform.localRotation = Quaternion.Euler(0, 0, 8.111f);

            Vector3 test2 = new Vector3(0, 0, -10);
            Plane6.transform.localPosition = test2;
            Plane6.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //  source7.Play();
        }
    }
    void Position()
    {
           Vector3 trans = new Vector3(-1.063f, 0.156f, -0.5067f);
           Pferdekopf.transform.localPosition = trans;
           Pferdekopf.transform.localRotation = Quaternion.Euler(0, 0, 0);
           Vector3 trans2 = Vector3.zero; 
           Vorderbeine.transform.localPosition = trans2;
           Vorderbeine.transform.localRotation = Quaternion.Euler(0, 0, 8.111f);
            Vector3 trans3 = Vector3.zero;
            Plane6.transform.localPosition = trans3;
            Plane6.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void playMusic()
    {
        source.PlayOneShot(audioC);
        played = true;
    }

    void parentingObjects()
    {
        Vorderbeine.transform.parent = Pferdekopf.transform;
        Plane6.transform.parent = Plane1.transform;
        Pferdekopf.gameObject.tag = "passt";
        Vorderbeine.gameObject.tag = "passt";
    }
    }


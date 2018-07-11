using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger5 : MonoBehaviour

{

    public GameObject KörperFlügel;
    public GameObject Pferdehinterbeine;

    public GameObject Plane2;
    public GameObject Plane4;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("oben4") && (!played))
        {
            playMusic();
            parentingObjects();
            Position();


            //Set LocalPositions in new Parents
            Vector3 test2 = new Vector3(0.0003999353f, -0.0002206423f, -0.00020004514f);
            KörperFlügel.transform.localPosition = test2;
            KörperFlügel.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Vector3 test3 = new Vector3(0.02000427f, 0, 9.979996f);
            Plane2.transform.localPosition = test3;
            Plane2.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }
    void Position()
    {
        Vector3 trans2 = Vector3.zero;
        KörperFlügel.transform.localPosition = trans2;
        KörperFlügel.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Vector3 trans3 = Vector3.zero;
        Plane2.transform.localPosition = trans3;
        Plane2.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void playMusic()
    {
        source.PlayOneShot(audioC);
        played = true;
    }
    void parentingObjects()
    {
        KörperFlügel.transform.parent = Pferdehinterbeine.transform;
        Plane2.transform.parent = Plane4.transform;
        Pferdehinterbeine.gameObject.tag = "passt";
        KörperFlügel.gameObject.tag = "passt";
      
    }
    }

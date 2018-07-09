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

    public Vector3 original;
    public Vector3 original2;
    public Vector3 hposition;
    public Vector3 mposition;
    
    void Start()
    {
        source =gameObject.GetComponent<AudioSource>();
       
    }
    void OnEnable()
    {
       original = Mitte.transform.position;
       original2 = Pferdehinterbeine.transform.position;
    }

    void Update()
    {
        
        //  Debug.Log("Mitte" + originalPosition);
        // Position();
        /*   hposition = Pferdehinterbeine.transform.position;
           Debug.Log("Pferdehinterbeine" + hposition);
           mposition = Mitte.transform.position;
           Debug.Log("Mitte1" + mposition); */
    } 

    void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("rechts4")&&(!played))
        {
           

            Debug.Log("True");
            Position();
            source.PlayOneShot(audioC);
            played = true;

            // Mitte.transform.SetParent(Group4, false);
            //Mitte.transform.parent = Group4;
            Pferdehinterbeine.transform.parent = Mitte.transform;
            Plane4.transform.parent = Plane5.transform;
            Mitte.gameObject.tag = "passt";
            Pferdehinterbeine.gameObject.tag = "passt";
            Position();

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
}

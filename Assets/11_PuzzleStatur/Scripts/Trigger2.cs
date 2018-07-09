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

    public Vector3 hposition;
    public Vector3 mposition;

    public GameObject Plane5;
    public GameObject Plane6;

    public Vector3 original;
    public Vector3 original2;
    void Start()
    {
      source=gameObject.GetComponent<AudioSource> ();

    }
    void Awake()
    {
        original = Mitte.transform.position;
        original2 = Vorderbeine.transform.position;
    }
    
    void Update()
    {
     //   Debug.Log("Mitte" + original);
     //   Debug.Log("Vorderbeine" + original2);
        //  Debug.Log("Vorderbeine" + original2);
        //  Position();
        /* hposition = Vorderbeine.transform.position;
         Debug.Log("Vorderbeine" + hposition);
         mposition = Mitte.transform.position;
         Debug.Log("Mitte" + mposition);*/
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
            Mitte.gameObject.tag = "passt";
            Vorderbeine.gameObject.tag = "passt";
            Position();

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
      //  Vector3 trans = new Vector3(-1.360001f, -0.1660004f, -0.5229996f);
      //  Vorderbeine.transform.localPosition = trans;
     //  Vorderbeine.transform.localRotation = Quaternion.Euler(0, 0, 8.111f);

        Vector3 trans2 = Vector3.zero;
        Mitte.transform.localPosition = trans2;
        Mitte.transform.localRotation = Quaternion.Euler(0, 0, 0);

        Vector3 trans3 = Vector3.zero;
       Plane5.transform.localPosition = trans3;
        Plane5.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
    }
        
    }

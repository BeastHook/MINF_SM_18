using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger4 : MonoBehaviour
{ 
 public GameObject Kopf;
public GameObject Pferdekopf;

    public GameObject Plane1;
    public GameObject Plane3;

    public AudioSource source;
    public AudioClip audioC;
    public bool played = false;

    public Vector3 original;
    public Vector3 original2;

    void Start()
    {
      source = gameObject.GetComponent<AudioSource>();      
    }
    void Awake()
    {
        original = Pferdekopf.transform.position;
        original2 = Kopf.transform.position;
    }
    void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("rechts3")&& (!played))
    {
            Debug.Log("True");
            
            source.PlayOneShot(audioC);
            played = true;
        //  Mitte.transform.SetParent(Group4, false);
        Kopf.transform.parent = Pferdekopf.transform;
        Plane3.transform.parent = Plane1.transform;
        Kopf.gameObject.tag = "passt";
        Pferdekopf.gameObject.tag = "passt";
        Position();
        Vector3 test = new Vector3(-1.164675e-05f, 0.002000022f, 0);
         Kopf.transform.localPosition = test;
         Kopf.transform.localRotation = Quaternion.Euler(0, 0, 0);

         Vector3 test2 = new Vector3(-10.03f, 0, 0.02000427f);
         Plane3.transform.localPosition = test2;
         Plane3.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
}
    void Position()
    {
        
        Vector3 trans = new Vector3(-1.063f, 0.156f, -0.5067f);
        Pferdekopf.transform.localPosition = trans;
        Pferdekopf.transform.localRotation = Quaternion.Euler(0, 0, 0);
        Vector3 trans2 = Vector3.zero;
        Kopf.transform.localPosition = trans2;
        // Vector3 trans2 = new Vector3(-0.02991999f, -0.0205959f, -0.10203f);
        Kopf.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //   Debug.Log("Mitte Final" + Mitte.transform.position);
        Vector3 trans3 = Vector3.zero;
        Plane3.transform.localPosition = trans3;
        Plane3.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}

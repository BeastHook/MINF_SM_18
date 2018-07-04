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

void Start()
{
      source = gameObject.GetComponent<AudioSource>();      
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
        Kopf.gameObject.tag = "loesung";
        Pferdekopf.gameObject.tag = "loesung";

        }
}
}

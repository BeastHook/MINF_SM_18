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

    public Vector3 original;
    public Vector3 original2;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();    
    }
    void Awake()
    {
        original = KörperFlügel.transform.position;
        original2 = Kopf.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rechts2")&&(!played))
        {
            Debug.Log("True");

            source.PlayOneShot(audioC);
            played = true;
            Position();
            //  Mitte.transform.SetParent(Group4, false);
            KörperFlügel.transform.parent = Kopf.transform;
            Plane2.transform.parent = Plane3.transform;
            Kopf.gameObject.tag = "passt";
            KörperFlügel.gameObject.tag = "passt";
            Position();

            Vector3 test2 = new Vector3(-0.0005919302f, -0.001911569f, -0.0002313424f);
            KörperFlügel.transform.localPosition = test2;
            KörperFlügel.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Vector3 test3 = new Vector3(-10.06f, 0, -0.02000427f);
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
}

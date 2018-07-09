using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger5 : MonoBehaviour

{


    //public Transform Group4;
    public GameObject KörperFlügel;
    public GameObject Pferdehinterbeine;

    public GameObject Plane2;
    public GameObject Plane4;

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
        original = KörperFlügel.transform.position;
        original2 = Pferdehinterbeine.transform.position;
    }
    void Update()
    {
        /*   hposition = Pferdehinterbeine.transform.position;
           Debug.Log("Pferdehinterbeine" + hposition);
           mposition = KörperFlügel.transform.position;
           Debug.Log("KörperFlügel" + mposition); */
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("oben4") && (!played))
        {
            Debug.Log("True");
            Position();
            source.PlayOneShot(audioC);
            played = true;

            //  Mitte.transform.SetParent(Group4, false);
            KörperFlügel.transform.parent = Pferdehinterbeine.transform;
            Plane2.transform.parent = Plane4.transform;
            Pferdehinterbeine.gameObject.tag = "passt";
            KörperFlügel.gameObject.tag = "passt";
            Position();

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
}

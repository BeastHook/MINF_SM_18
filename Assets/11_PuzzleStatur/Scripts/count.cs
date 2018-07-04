using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour {
    
    //public GameObject L;
    public Material   Skulptur;
    public GameObject Mitte;
    public GameObject Pferdehinterbeine;
    public GameObject Vorderbeine;
    public GameObject KörperFlügel;
    public GameObject Kopf;
    public GameObject Pferdekopf;
    public GameObject RENOMME;
 

    public GameObject tresure_box;
    //public GameObject box_top;


    public Animation anim;
    public AnimationClip animationclip;
    public bool isPlaying = false;


    public AudioSource source;
    public bool played = false;
    public AudioClip audioClip;

    public Quaternion myValue;
    // public Material Rot;
    // Use this for initialization
    void Start () {
        //L.SetActive(false);
        


        RENOMME.SetActive(false);
        tresure_box.SetActive(false);
        // GetComponent<Renderer>().material = Rot;
        source = gameObject.GetComponent<AudioSource>();
       
    


}
	
	
	void Update () {
        if (GameObject.FindGameObjectsWithTag("loesung").Length == 6 && (!played) && (!isPlaying))
        {
            // L.SetActive(true);

            // BOX
            anim.GetComponent<Animation>().Play();
            isPlaying = true;
            tresure_box.SetActive(true);
            Debug.Log("FEUER");
            source.PlayOneShot(audioClip);
            played=true;
            puzzleTeile();

        }
        if (Mitte.gameObject.tag == "loesung")
        {
            Mitte.GetComponent<Renderer>().material = Skulptur;
            


        }
        if (Pferdehinterbeine.gameObject.tag == "loesung")
        {
            Pferdehinterbeine.GetComponent<Renderer>().material = Skulptur;
            

        }
        if (Vorderbeine.gameObject.tag == "loesung")
        {
            Vorderbeine.GetComponent<Renderer>().material = Skulptur;
            

        }
        if (KörperFlügel.gameObject.tag == "loesung")
        {
            KörperFlügel.GetComponent<Renderer>().material = Skulptur;
            

        }
        if (Kopf.gameObject.tag == "loesung")
        {
            Kopf.GetComponent<Renderer>().material = Skulptur;
            

        }
        if (Pferdekopf.gameObject.tag == "loesung")
        {
            Pferdekopf.GetComponent<Renderer>().material = Skulptur;
            
        }

    }
    
      void puzzleTeile()
    {
        Mitte.SetActive(false);
        Pferdehinterbeine.SetActive(false);
        Vorderbeine.SetActive(false);
        KörperFlügel.SetActive(false);
        Kopf.SetActive(false);
        Pferdekopf.SetActive(false);
        RENOMME.SetActive(true);
        
    }
    void LateUpdate()
    {
        tresure_box.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        
    }
  }


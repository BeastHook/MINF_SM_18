﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour {
    //Meshes
    public Material   Skulptur;
    public GameObject Mitte;
    public GameObject Pferdehinterbeine;
    public GameObject Vorderbeine;
    public GameObject KörperFlügel;
    public GameObject Kopf;
    public GameObject Pferdekopf;
    public GameObject RENOMME;
    public GameObject tresure_box;
    //Animation
    public Animation anim;
    public AnimationClip animationclip;
    public bool isPlaying = false;
    public Quaternion myValue;
    //Audio
    public AudioSource source;
    public bool played = false;
    public AudioClip audioClip;

    void Start () {
        RENOMME.SetActive(false);
        tresure_box.SetActive(false);
        source = gameObject.GetComponent<AudioSource>();
    }

    IEnumerator SceneFinish()
    {
        yield return new WaitForSeconds(10);
        MultisceneManager.Instance.StartCoroutine(MultisceneManager.Instance.FinishLevel(true));
    }

    void Update()
    {
        //Lösung
        if (GameObject.FindGameObjectsWithTag("passt").Length == 6 && (!played) && (!isPlaying))
        {
            boxOpen();
            musicPlay();
            puzzleTeile();

        }
        if (GameObject.FindGameObjectsWithTag("passt").Length == 5 && GameObject.FindGameObjectsWithTag("passt2").Length == 1 && (!played) && (!isPlaying))
        {
            boxOpen();
            musicPlay();
            puzzleTeile();
        }
            //Material
            if (Mitte.gameObject.tag == "passt")
        {
            Mitte.GetComponent<Renderer>().material = Skulptur;

        }
        if (Pferdehinterbeine.gameObject.tag == "passt")
        {
            Pferdehinterbeine.GetComponent<Renderer>().material = Skulptur;

        }
        if (Vorderbeine.gameObject.tag == "passt")
        {
            Vorderbeine.GetComponent<Renderer>().material = Skulptur;
        }
        if (KörperFlügel.gameObject.tag == "passt")
        {
            KörperFlügel.GetComponent<Renderer>().material = Skulptur;

        }
        if (Kopf.gameObject.tag == "passt")
        {
            Kopf.GetComponent<Renderer>().material = Skulptur;

        }
        if (Pferdekopf.gameObject.tag == "passt")
        {
            Pferdekopf.GetComponent<Renderer>().material = Skulptur;

        }
        if (Kopf.gameObject.tag == "passt2")
        {
            Kopf.GetComponent<Renderer>().material = Skulptur;
        }

    }
      
    void LateUpdate()
    {
        
        tresure_box.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        tresure_box.transform.localRotation = Quaternion.Euler(0, 180, 0);


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
    void boxOpen()
    {
        anim.GetComponent<Animation>().Play();
        isPlaying = true;
        tresure_box.SetActive(true);
        Vector3 trans = new Vector3(0.884f, 0.91f, -1.095f);
        tresure_box.transform.localPosition = trans;
        StartCoroutine(SceneFinish());
    }
    void musicPlay()
    {
        source.PlayOneShot(audioClip);
        played = true;
    }
}


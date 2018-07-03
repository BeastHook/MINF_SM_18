using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeControll : MonoBehaviour {

    public Animator anim;
    public GameObject trigger;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        trigger = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}


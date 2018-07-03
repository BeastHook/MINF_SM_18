using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torController : MonoBehaviour {

    public Animator ani;
    public GameObject trigger;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        trigger = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}

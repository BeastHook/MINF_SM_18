using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayer : MonoBehaviour {

    private GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upplatfrom : MonoBehaviour {
    public GameObject player;
    
    private Vector3 posB;
    private Vector3 nexPos;
    [SerializeField]
    private float platformSpeed = 1;
    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transformB;
    // Use this for initialization
    public bool move = false;
    void Start()
    {
        
        posB = transformB.localPosition;
        nexPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
       if(move)
        Move();

    }
    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPos, platformSpeed * Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition, nexPos) <= 0.001)
        {
            player.GetComponent<AutomatedMovement>().moveallowed = true;
            move = false;
        }
    }
  
}

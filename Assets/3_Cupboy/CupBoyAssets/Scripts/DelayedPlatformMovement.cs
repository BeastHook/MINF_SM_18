using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedPlatformMovement : MonoBehaviour {

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nexPos;
    [SerializeField]
    private float platformSpeed, prevDistance;
    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transformB;
    [SerializeField]
    private float waitTime;
    public bool moveAllowed = true;
    private float speed;
    // Use this for initialization
    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Camera.main.transform.position;
        float distance = pos.magnitude;
        float dif = Mathf.Abs(prevDistance - distance);
       // Debug.Log(dif);
        if (dif > 0.004f)//distance != prevDistance)
        {
            speed = platformSpeed;
            Debug.Log("yay");
        }
        else
        {
            speed = 0;
        }

        if (moveAllowed)
        {
            Move();
        }
        prevDistance = distance;


    }

    private void Update()
    {

        Move();
    }
    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPos, speed * Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition, nexPos) <= 0.0001)
        {
            StartCoroutine("Wait");
            changeDestination();
        }
    }
    private void changeDestination()
    {
        nexPos = nexPos != posA ? posA : posB;
    }
    IEnumerator Wait()
    {
        Debug.Log("start");

        moveAllowed = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("end");
        moveAllowed = true;

    }
}

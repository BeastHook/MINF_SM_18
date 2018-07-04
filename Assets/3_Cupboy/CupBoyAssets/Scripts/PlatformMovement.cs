using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nexPos;
    [SerializeField]
    private float platformSpeed,prevDistance;
    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private Transform transformB;
    public bool moveAllowed =true;
    public bool maxreach = false;
    private float speed;
	// Use this for initialization
	void Start () {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 pos = Camera.main.transform.position;
        float distance = pos.magnitude;
        //Debug.Log(distance);
        //platformSpeed = distance * 0.1f;
        float dif = Mathf.Abs(prevDistance - distance);

        if (dif>0.021f)//distance != prevDistance)
        {
            speed = platformSpeed * dif * 90;
        }
        else
        {
            speed=0;
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
        if (Vector3.Distance(childTransform.localPosition, nexPos) <= 0.0001) {
           //StartCoroutine("Wait");
            changeDestination();
            maxreach = true;
        }
    }
    private void changeDestination() {
        nexPos = nexPos != posA ? posA : posB;
    }
    IEnumerator Wait()
    {
        Debug.Log("start");

        moveAllowed = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("end");
        moveAllowed = true;
        
    }
}

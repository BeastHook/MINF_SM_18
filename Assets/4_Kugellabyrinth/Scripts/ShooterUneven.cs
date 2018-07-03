using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterUneven : MonoBehaviour {

    public GameObject bullet;
    public Transform gun;
    public float shootRate = 0f;
    public float speed = 0f;
    private float shootRateTimeStamp = 2f;
    public Transform target;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Target").transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > shootRateTimeStamp)
        {
            GameObject go = (GameObject)Instantiate(
                bullet, gun.position, gun.rotation);
            //go.GetComponent<Rigidbody>().AddForce(gun.forward * speed);
            shootRateTimeStamp = shootRateTimeStamp + shootRate;
            go.transform.position = Vector3.MoveTowards(go.transform.position, target.position, speed*Time.deltaTime);
            //bullet.transform.SetParent(gun.transform);

        }
    }
}

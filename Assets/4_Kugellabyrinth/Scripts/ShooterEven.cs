using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEven : MonoBehaviour {

    public GameObject bullet;
    public Transform gun;
    public float shootRate = 0f;
    public float shootForce = 0f;
    private float shootRateTimeStamp = 4f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > shootRateTimeStamp)
        {
            GameObject go = (GameObject)Instantiate(
                bullet, gun.position, gun.rotation);
            go.GetComponent<Rigidbody>().AddForce(gun.forward * shootForce);
            shootRateTimeStamp = shootRateTimeStamp + shootRate;

        }
    }
}

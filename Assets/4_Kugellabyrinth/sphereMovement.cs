using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class sphereMovement : MonoBehaviour {

    public Transform target;
    public float speed;
    public GameObject coconutSpawn;
    public Collider m_Collider;
    public Renderer rend;
    public bool update;

	private void Awake()
	{
		DefaultTrackableEventHandler.OnTrackingEvent += OnTracking;
	}

	private void OnDestroy()
	{
		DefaultTrackableEventHandler.OnTrackingEvent -= OnTracking;
	}

	private void OnTracking(bool trackingfound)
	{
		update = trackingfound;
		gameObject.SetActive(trackingfound);
	}


	// Use this for initialization
    void Start () {

        
        m_Collider = GetComponent<Collider>();
        rend.GetComponent<Renderer>();
        m_Collider.enabled = false;
        rend.enabled = false;
        update = false;

    }

    // Update is called once per frame

    void Update() {
        
        {
            if ((Time.time % 5) >= 0 && (Time.time % 5) <= 0.1)
            {
                transform.position = coconutSpawn.transform.position;
                if (update)
                {
                    m_Collider.enabled = true;
                    rend.enabled = true;
                }
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        
    }
    

    void OnCollisionEnter(Collision collision)
    {      
            m_Collider.enabled = false;
            rend.enabled = false;
       
    }
}

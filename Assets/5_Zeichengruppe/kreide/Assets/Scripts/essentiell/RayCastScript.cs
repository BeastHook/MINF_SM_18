using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{

    public bool mouse = false;
    float holdDur = 3.5f;
    float timer = 0;

    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    //protected Camera m_Camera;
    protected List<Vector2> m_Points;

    public GameObject pointer;
    public GameObject toInst;
    public GameObject visualizer;

    //public GameObject boden;
    public GameObject hintergrund;
    //public GameObject figur;

	public GameObject drawing;
    public GameObject tester;

    public static bool buttonPressed = false;

    public LineRenderer lineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }

    public bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public EdgeCollider2D edgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }

    public List<Vector2> points
    {
        get
        {
            return m_Points;
        }
    }

    void Update()
    {
        visualizer.SetActive(false);

        Debug.DrawRay(pointer.transform.position, pointer.transform.forward, Color.red);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(pointer.transform.position, pointer.transform.forward); // pointer.transform.up
        bool backgroundHit = false;

        foreach(RaycastHit hit in hits)
        {

            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.name == hintergrund.name)
            {
                backgroundHit = true;
                //Debug.Log("Ausgabe: " + hit.transform.gameObject.name);
                Debug.Log("<color=pink>MultiTarget trifft Hintergrund!</color>");

                visualizer.SetActive(true);

                visualizer.transform.position = hit.point;

                // Hier Array! Und Reset! ???
                //if (Input.GetMouseButton(0))
                if (Input.GetButton("Fire1"))
                {
                    GameObject point =  Instantiate(toInst, hit.point, new Quaternion()) as GameObject;
					point.transform.SetParent (drawing.transform);
                }
            }
        }

        if (!backgroundHit)
        {
            if (mouse)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    timer = Time.time;
                }
                else if (Input.GetMouseButton(0))
                {
                    if (Time.time - timer > holdDur)
                    {
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;
                        Debug.Log("Reset wurde gecalled!!");
                        ////// HIER DIE RESET ANBINDEN!!!!
						GetComponent<LevelController>().ResetButtonPressed();
					
                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    timer = Time.time;
                }
                else if (Input.GetButton("Fire1"))
                {
                    if (Time.time - timer > holdDur)
                    {
                        //by making it positive inf, we won't subsequently run this code by accident,
                        //since X - +inf = -inf, which is always less than holdDur
                        timer = float.PositiveInfinity;


                    }
                }
                else
                {
                    timer = float.PositiveInfinity;
                }
            }
        }

        }

    /* Mit 2D RayCaster!!
     
    	void Update ()
		{
		visualizer.SetActive (false);
		//Ray ray = new Ray (pointer.transform.position, pointer.transform.forward);

		//hier und bei hits.RaycastAll hab ich das Minus von pointer.transform.up weggenommen. Jetzt malt er auf jeden Fall in die richtige Richtung
		Debug.DrawRay (pointer.transform.position, -pointer.transform.up, Color.red);
  
		RaycastHit2D[] hits;
		hits = Physics2D.RaycastAll(pointer.transform.position, -pointer.transform.up, 10.0F);
		Debug.Log ("collected hits");

		//for (int i = 0; i < hits.Length; i++)
		foreach (RaycastHit2D hit in hits)
		{
			Debug.Log ("In hits!");
            Debug.Log("Hit: " + hit.collider.name);

			if(hit.collider.name == hintergrund.name){
				Debug.Log ("Ausgabe: "+hit.transform.gameObject.name);

				visualizer.SetActive (true);
				visualizer.transform.position = hit.point;

                // Hier Array! Und Reset! ???
				if (Input.GetMouseButton (0)) {
					Instantiate (toInst, hit.point, new Quaternion ());
				}
			}
		
		}

	}*/

    void Reset()
    {
        if (m_LineRenderer != null)
        {
            m_LineRenderer.positionCount = 0;
        }
        if (m_Points != null)
        {
            m_Points.Clear();
        }
        if (m_EdgeCollider2D != null && m_AddCollider)
        {
            m_EdgeCollider2D.Reset();
        }
    }



    void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
    }

}
 
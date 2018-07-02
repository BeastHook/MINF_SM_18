using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHitOld : MonoBehaviour
{

    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    protected List<Vector2> m_Points;

    public GameObject pointer;
    public GameObject toInst;
    public GameObject visualizer;
    public GameObject virtualButton;

    public GameObject boden;
    public GameObject hintergrund;
    public GameObject figur;

    public GameObject tester;

    private string bodenName, hintergrundName, figurname;

    public static bool buttonPressed = false;

    public virtual LineRenderer lineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }

    public virtual bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public virtual EdgeCollider2D edgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }

    public virtual List<Vector2> points
    {
        get
        {
            return m_Points;
        }
    }

    protected virtual void Awake()
    {

        bodenName = boden.name;
        hintergrundName = hintergrund.name;
        figurname = figur.name;

        Debug.LogWarning(bodenName + " " + hintergrundName + " " + figurname);

        if (m_LineRenderer == null)
        {
            Debug.LogWarning("DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer.");
            CreateDefaultLineRenderer();
        }
        if (m_EdgeCollider2D == null && m_AddCollider)
        {
            Debug.LogWarning("DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D.");
            CreateDefaultEdgeCollider2D();
        }
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();
    }

    protected virtual void FixedUpdate()
    {

        visualizer.SetActive(false);
        tester.SetActive(false);

        RaycastHit hit;
        Ray ray = new Ray(pointer.transform.position, pointer.transform.forward);

        Debug.DrawRay(pointer.transform.position, pointer.transform.forward, Color.red);


        RaycastHit[] hits;
        hits = Physics.RaycastAll(pointer.transform.position, pointer.transform.forward, 100.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hitAll = hits[i];

            if (hitAll.collider.name == hintergrundName || hitAll.collider.name == bodenName || hitAll.collider.name == figurname)
            {
                Debug.Log("Ausgabe: " + hitAll.transform.gameObject.name);

                visualizer.SetActive(true);
                tester.SetActive(true);

                visualizer.transform.position = hitAll.point;

                if (Input.GetMouseButton(0))
                {
                    //if(virtualButton.)
                    Instantiate(toInst, hitAll.point, new Quaternion());
                }
            }

        }

    }

    protected virtual void Reset()
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

    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 0;
        m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.useWorldSpace = true;
    }

    protected virtual void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
    }

}
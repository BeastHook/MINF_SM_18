using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {

	private LineRenderer line;
	private Vector3 mousePosition;

	[SerializeField] private bool simplifyLine = false;
	[SerializeField] private float simplifyTolerance = 0.02f;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			line.positionCount++;
			line.SetPosition (line.positionCount - 1, mousePosition);
		}

		if (Input.GetMouseButtonUp (0)) {
			if (simplifyLine) {
				line.Simplify (simplifyTolerance);
			}
			enabled = false;
		}
	}
}

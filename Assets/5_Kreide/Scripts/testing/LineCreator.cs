using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour {

	[SerializeField] private GameObject line;
	private Vector2 mousePosition;
	public GameObject parentObject;
	
	List<GameObject> linesList = new List<GameObject>();

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			
			Debug.Log("Line!");
			
			// Initiate Object
			GameObject newLine = Instantiate (line, mousePosition, Quaternion.Euler (0.0f, 0.0f, 0.0f)) as GameObject;
			newLine.transform.parent = parentObject.transform;
			
			// Create Collider
			
			//BoxCollider boxCollider = newLine.AddComponent<BoxCollider>();
			
			BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider> ();
			col.transform.parent = newLine.transform; // Collider is added as child object of line
			
			// _bc.center = Vector3.zero;
			
			linesList.Add(newLine); // Adds Line to List
			

		}
	}
}

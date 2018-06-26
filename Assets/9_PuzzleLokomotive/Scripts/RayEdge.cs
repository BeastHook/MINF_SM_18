using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEdge : MonoBehaviour {
	
	private GameObject raycaster1;
	private GameObject raycaster2;

	private GameObject matchingColEdge;
	private bool connected;

	void Awake(){
		connected = false;
		foreach(Transform child in transform){
			if(raycaster1 == null){
				raycaster1 = child.gameObject;
				// Debug.Log("This raycaster: " + child.name);
				// Debug.Log("This matchingColName: " + child.GetComponent<Raycaster>().getMatchingColName());
				// string matchingColName = child.GetComponent<Raycaster>().getMatchingColName();
				// Debug.Log("This matchingCol: " + GameObject.Find(matchingColName).name);
				// Debug.Log("This matchingColEdge: " + GameObject.Find(child.GetComponent<Raycaster>().getMatchingColName()).transform.parent.gameObject.name);
				matchingColEdge = GameObject.Find(child.GetComponent<Raycaster>().getMatchingColName()).transform.parent.gameObject;
				// Debug.Log(gameObject.name+" gehört zu " + matchingColEdge);
			} else {
				raycaster2 = child.gameObject;
			}
		}
	}

	void Update(){
		// Beide Raycaster checken, ob sie mit dem richtigen Collider verbunden sind
		if(raycaster1.GetComponent<Raycaster>().isConnected() && (raycaster2.GetComponent<Raycaster>().isConnected())){
			connected = true;
			// Debug.Log (gameObject.name +" meldet: mit "+matchingColEdge.name+" verbunden!");
	
		}
	}
	public bool isConnected(){
		return connected;
	}

	public GameObject getRaycaster1(){
		return raycaster1;
	}

	public GameObject getRaycaster2(){
		return raycaster2;
	}

	public GameObject getMatchingColEdge(){
		return matchingColEdge;
	}

	// NewCenter ist die Position, die jetzt das neue Zentrum wird;
	public void setNewPosition(Vector3 newCenter, Vector3 newCenterRotation){
		gameObject.transform.parent.localPosition = new Vector3(0,0,0);
			if(newCenterRotation.y == 0){
			switch(gameObject.name){
				case "RayEdge1" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge2" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge3" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge4" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge5" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge6" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge7" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge8" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge9" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge10" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge11" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge12" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
			}
		// Debug.Log("Neues Zentrum: "+newCenter+ " Raycaster wurde umpositioniert auf: " + transform.localPosition);
		} else if (newCenterRotation.y == 270){
			switch(gameObject.name){
				case "RayEdge1" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge2" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge3" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge4" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge5" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge6" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge7" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge8" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge9" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge10" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge11" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge12" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
			}
		} else if (newCenterRotation.y == 180){
switch(gameObject.name){
				case "RayEdge1" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge2" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge3" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge4" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge5" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge6" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge7" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge8" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge9" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge10" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge11" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge12" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
			}
		} else if (newCenterRotation.y == 90){
switch(gameObject.name){
				case "RayEdge1" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge2" : transform.localPosition = new Vector3(0, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge3" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge4" : transform.localPosition = new Vector3(0, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge5" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge6" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge7" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "RayEdge8" : transform.localPosition = new Vector3(1, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "RayEdge9" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge10" : transform.localPosition = new Vector3(2, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "RayEdge11" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "RayEdge12" : transform.localPosition = new Vector3(2, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
			}
		}
	}
}

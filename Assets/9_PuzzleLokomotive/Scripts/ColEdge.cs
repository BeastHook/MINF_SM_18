using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColEdge: MonoBehaviour {
	private GameObject collider1;
	private GameObject collider2;

	void Start(){
		foreach(Transform child in transform){
			if(collider1 == null){
				collider1 = child.gameObject;
			} else {
				collider2 = child.gameObject;
			}
		}
	}

	public void setCollider1(GameObject col){
		collider1 = col;
	}

	public void setCollider2(GameObject col){
		collider2 = col;
	}

	public GameObject getCollider1(){
		return collider1;
	}

	public GameObject getCollider2(){
		return collider2;
	}

	
	public void setNewPosition(Vector3 newCenter, Vector3 newCenterRotation){
		// Debug.Log("NewCenter: "+ newCenter); 
		gameObject.transform.parent.localPosition = new Vector3(0,0,0);
		if(newCenterRotation.y == 0){
			switch(gameObject.name){
				case "ColEdge1" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge2" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge3" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge4" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge5" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge6" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge7" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge8" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge9" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge10" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge11" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge12" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
			}
		} else if (newCenterRotation.y == 270){
			switch(gameObject.name){
				case "ColEdge1" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge2" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge3" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge4" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge5" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge6" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge7" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge8" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge9" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge10" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge11" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge12" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
			}
		} else if (newCenterRotation.y == 270){
			switch(gameObject.name){
				case "ColEdge1" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge2" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge3" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge4" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge5" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge6" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge7" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge8" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge9" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge10" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge11" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge12" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
			}
		} else if (newCenterRotation.y == 180){
			switch(gameObject.name){
				case "ColEdge1" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge2" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge3" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge4" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge5" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge6" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge7" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge8" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge9" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge10" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge11" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge12" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
			}
		} else if (newCenterRotation.y == 90){
			switch(gameObject.name){
				case "ColEdge1" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge2" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge3" : transform.localPosition = new Vector3(0, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge4" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge5" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge6" : transform.localPosition = new Vector3(1, 0, 2) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge7" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge8" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
				case "ColEdge9" : transform.localPosition = new Vector3(1, 0, 0) - newCenter; transform.localEulerAngles = new Vector3(0, -90, 0)+newCenterRotation; break;
				case "ColEdge10" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 0, 0)+newCenterRotation; break;
				case "ColEdge11" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 90, 0)+newCenterRotation; break;
				case "ColEdge12" : transform.localPosition = new Vector3(2, 0, 1) - newCenter; transform.localEulerAngles = new Vector3(0, 180, 0)+newCenterRotation; break;
			}
		}
	}
}

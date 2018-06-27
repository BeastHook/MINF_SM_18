using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

	public GameObject piece;
	GameObject otherPiece;

	private string matchingColName;
	private bool connected;

	// Use this for initialization
	void Start () {		
		matchingColName = "nothing";
		connected = false;
		switch(gameObject.name){
			case "RayCaster1" : matchingColName = "Collider1"; break;
			case "RayCaster2" : matchingColName = "Collider2"; break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distance ;

		Vector3 forward = transform.TransformDirection(Vector3.forward);
				Ray ray = new Ray(gameObject.transform.position, forward);
				Debug.DrawRay(ray.origin, ray.direction *0.02f, Color.green, 10, false);

					if(Physics.Raycast(ray, out hit, 0.2f)){ // 0.02 ist füs Erste die Reichweite des Raycasts
							// Debug.Log(hit.transform.name + " wurde getroffen!: Abstand: " + hit.distance);		

							// Prüfen, ob der richtige Collider getroffen wurde.
							if(hit.transform.name == matchingColName){
								connected = true;
								// Debug.Log (" Passendes Puzzleteil: "+hit.transform.parent.parent + "Collider: "+matchingColName);
							}
							// TODO: Position so verändern, dass die Puzzleteile genau zusammenpassen
							// Dazu: Anderes PuzzleTeil bekommen
								foreach (Transform child in hit.transform.parent) 
								{
									if (child.name != hit.transform.name)
									{
											otherPiece = child.gameObject;
									}
								}
							// Position der Puzzleteile so anpassen, dass die Collider sich genau berühren
					} else {
						connected = false;
					}
	}

	public bool checkConnection(){
		return connected;
	}

	public GameObject getOtherPiece(){
		return otherPiece;
	}


	void OnTriggerEnter( Collider col){
		// Debug.Log("Auto wurde zusammengebaut");
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour {


	private string matchingColName;
	private bool connected;

	// Use this for initialization
	void Awake () {		
		matchingColName = "nothing";
		connected = false;

    // Festlegen, zu welchem Collider der Raycaster gehört
		// TODO: Feld korrekt aufstellen und Raycaster zu Collidern zuordnen
		switch(gameObject.name){
			case "RayCaster1" : matchingColName = "Collider1"; break;
			case "RayCaster2" : matchingColName = "Collider2"; break;
			case "RayCaster3" : matchingColName = "Collider14"; break;
			case "RayCaster4" : matchingColName = "Collider13"; break;
			case "RayCaster5" : matchingColName = "Collider5"; break;
			case "RayCaster6" : matchingColName = "Collider6"; break;
			case "RayCaster7" : matchingColName = "Collider8"; break;
			case "RayCaster8" : matchingColName = "Collider7"; break;
			case "RayCaster9" : matchingColName = "Collider4"; break;
			case "RayCaster10" : matchingColName = "Collider3"; break;
			case "RayCaster11" : matchingColName = "Collider15"; break;
			case "RayCaster12" : matchingColName = "Collider16"; break;
			case "RayCaster13" : matchingColName = "Collider21"; break;
			case "RayCaster14" : matchingColName = "Collider22"; break;
			case "RayCaster15" : matchingColName = "Collider10"; break;
			case "RayCaster16" : matchingColName = "Collider9"; break;
			case "RayCaster17" : matchingColName = "Collider18"; break;
			case "RayCaster18" : matchingColName = "Collider17"; break;
			case "RayCaster19" : matchingColName = "Collider20"; break;
			case "RayCaster20" : matchingColName = "Collider19"; break;
			case "RayCaster21" : matchingColName = "Collider24"; break;
			case "RayCaster22" : matchingColName = "Collider23"; break;
			case "RayCaster23" : matchingColName = "Collider11"; break;
			case "RayCaster24" : matchingColName = "Collider12"; break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		float distance ;

		Vector3 forward = transform.TransformDirection(Vector3.forward);
			Ray ray = new Ray(gameObject.transform.position, forward);
			Debug.DrawRay(ray.origin, ray.direction *0.2f, Color.green, 10, false);

		if(Physics.SphereCast(transform.position, 0.2f , transform.forward, out hit, 0.2f)){ // 0.02 ist füs Erste die Reichweite des Raycasts
				// Prüfen, ob der richtige Collider getroffen wurde.
				if(hit.transform.name == matchingColName){
					connected = true;
					// Debug.Log (gameObject.name +" meldet "+matchingColName+" getroffen!");
					startSparkles();
				}
		} else {
			connected = false;
			resetSparkles();
		}
	}

	public void OnCollisionEnter (Collision col){
		if(col.gameObject.name == matchingColName){
			connected = true;
								startSparkles();	
		}
	}

		public void OnCollisionStay (Collision col){
		if(col.gameObject.name == matchingColName){
			connected = true;
								startSparkles();	
		}
	}

		public void OnCollisionExit (Collision col){
		if(col.gameObject.name == matchingColName){
			connected = false;
			resetSparkles();	
		}
	}

	public void OnTriggerEnter (Collider col){
		if(col.gameObject.name == matchingColName){
			connected = true;
								startSparkles();
			
		}
	}

		public void OnTriggerStay (Collider col){
		if(col.gameObject.name == matchingColName){
			connected = true;
								startSparkles();
		}
	}
	public string getMatchingColName(){
		Awake();
		return matchingColName;
	}
	public bool isConnected(){
		return connected;
	}

	public void startSparkles(){
		foreach(Transform sparkle in GameObject.Find(matchingColName).transform){
			if(sparkle.tag  == "Sparkle"){
				sparkle.gameObject.SetActive(true);
			}
		}
	}

	public void resetSparkles(){
		foreach(Transform sparkle in GameObject.Find(matchingColName).transform){
			if(sparkle.tag  == "Sparkle"){
				sparkle.gameObject.SetActive(false);
			}
		}
	}

}


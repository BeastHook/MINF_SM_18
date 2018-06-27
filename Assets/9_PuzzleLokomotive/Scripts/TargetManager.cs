using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour {

	public float timeSet;
	public float timeTilRotation;
	public GameObject [] targets;
	// public GameObject treasureChest;
	public GameObject hint;
	public GameObject whiteSmoke;

	GameObject joint;
	GameObject center;
	GameObject axisFront;
	GameObject axisBack;

	public GameObject camera;
	// public Animator anim;
	public bool hasWon= false;
	int filledTargets;

	public bool chestOpened;
	GameObject[] ceilingPieces;
	GameObject [] yellowWagonPieces;
	GameObject [] frontWheels;
	GameObject [] backWheels;
	float openingTime = 2;
	bool firstHintWasPlayed;
	public GameObject firstHintSource;
	// Use this for initialization
	void Start () {
		filledTargets = 0;
		timeTilRotation = timeSet;
		// treasureChest.SetActive(false);
		hint.SetActive(false);
		ceilingPieces = GameObject.FindGameObjectsWithTag("Dach");
		yellowWagonPieces = GameObject.FindGameObjectsWithTag("YellowWaggon");
		frontWheels = GameObject.FindGameObjectsWithTag("FrontWheels");
		backWheels = GameObject.FindGameObjectsWithTag("BackWheels");
		joint = GameObject.Find("ChestJoint");
		center = GameObject.Find("Center");
		axisFront = GameObject.Find("FrontAxis");
		axisBack = GameObject.Find("BackAxis");
		whiteSmoke.SetActive(false);
		firstHintWasPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject lastTarget = null;
		foreach(GameObject target in targets){
			if(target.transform.childCount == 0 && !firstHintWasPlayed){
				firstHintSource.GetComponent<AudioSource>().Play();
				firstHintWasPlayed = true;
			}
			if(target.transform.childCount > 0){
				filledTargets ++;
				lastTarget = target;
			}
		}
			if(!hasWon && filledTargets == 1){
			// if(hasWon){
			Debug.Log("SPIEL GEWONNEN, JEEEEJ :) ");
			// treasureChest.SetActive(true);
			// treasureChest.transform.parent = lastTarget.transform;
			// switch(lastTarget.name){
			// 	case "ImageTarget1": treasureChest.transform.localPosition = new Vector3(-2,0, 1); treasureChest.transform.localEulerAngles = new Vector3(0,180,0);break;
			// 	case "ImageTarget3": treasureChest.transform.localPosition = new Vector3(2,0, 1); break;
			// 	case "ImageTarget5": treasureChest.transform.localPosition = new Vector3(-3,0, 0); treasureChest.transform.localEulerAngles = new Vector3(0,180,0);break;
			// 	case "ImageTarget7": treasureChest.transform.localPosition = new Vector3(-2,0, -1); treasureChest.transform.localEulerAngles = new Vector3(0,180,0); break;
			// 	case "ImageTarget9": treasureChest.transform.localPosition = new Vector3(2,0, -1); break;
			// }
			GetComponent<AudioSource>().Play();
			// TODO: Buchstabe für das Rätsel ausgeben
			// TODO: Spiel wieder zurücksetzen
			whiteSmoke.SetActive(true);
			hasWon = true;
			chestOpened = true;
		} else {
				filledTargets = 0;
		}

		// Objekte rotieren
		timeTilRotation -= Time.deltaTime;
			int index = Random.Range(0, targets.Length);
			if(timeTilRotation <= 0){
				timeTilRotation = timeSet;
				rotateObject(index);
			}

			if(chestOpened){
				hint.SetActive(true);				
				openingTime -= Time.deltaTime;
				foreach(GameObject piece in ceilingPieces){
					piece.transform.RotateAround(joint.transform.position, joint.transform.forward, 50 * Time.deltaTime);
				}
				if(openingTime <= 0){
					openingTime = 0;
					chestOpened = false;
				}

				foreach(GameObject piece in yellowWagonPieces){
					piece.transform.RotateAround(center.transform.position, center.transform.up, 20 * Time.deltaTime);
				}

					foreach(GameObject piece in frontWheels){
					piece.transform.RotateAround(center.transform.position, center.transform.up, 20 * Time.deltaTime);
					piece.transform.RotateAround(axisFront.transform.position, axisFront.transform.up, -50 * Time.deltaTime);
				}

					foreach(GameObject piece in backWheels){
					piece.transform.RotateAround(center.transform.position, center.transform.up, 20 * Time.deltaTime);						
					piece.transform.RotateAround(axisBack.transform.position, axisBack.transform.up, -50 * Time.deltaTime);
					
				}
			}
	}

	public void rotateObject(int index){
		GameObject thisPiece = targets[index];
		if(thisPiece.transform.childCount <= 4) {
			thisPiece.GetComponent<Target>().rotateTarget();
			Debug.Log("Object " + (index+1) + " was rotated");
		}
	}

	public void openChest(){

	}
}

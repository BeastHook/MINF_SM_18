using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;



public class Target : MonoBehaviour, ITrackableEventHandler
{

  // public List<GameObject> matchingTargets;
  public List<GameObject> colEdges;
  public List<GameObject> rayEdges;
  GameObject connectedTarget;
  GameObject connectedEdge;
  GameObject actualEdge;
  bool targetConnected;

	Vector3 defaultPosition;

  float rotationY = 0;

  ImageTargetBehaviour track;

  // Use this for initialization
  void Awake()
  {
    //		Debug.Log("So viele Rayedges hat "+ gameObject.name + ": "+rayEdges.Count);
    track = GetComponent<ImageTargetBehaviour>();
 


    if (track)
    {
      track.RegisterTrackableEventHandler(this);
    }

		switch(gameObject.name){
			case "ImageTarget1" : defaultPosition = new Vector3(0,0,0); break;
			case "ImageTarget2" : defaultPosition = new Vector3(1,0,0); break;
			case "ImageTarget3" : defaultPosition = new Vector3(2,0,0); break;
			case "ImageTarget4" : defaultPosition = new Vector3(0,0,1); break;
			case "ImageTarget5" : defaultPosition = new Vector3(1,0,1); break;
			case "ImageTarget6" : defaultPosition = new Vector3(2,0,1); break;
			case "ImageTarget7" : defaultPosition = new Vector3(0,0,2); break;
			case "ImageTarget8" : defaultPosition = new Vector3(1,0,2); break;
			case "ImageTarget9" : defaultPosition = new Vector3(2,0,2); break;
		}
    }

  // Update is called once per frame
  void Update()
  {
    if (transform.childCount == 0)
    {
      GetComponent<ImageTargetBehaviour>().enabled = false;
    }
		setEdges();
    // rearrangeOwnChildren();

    // Jeder RayEdge im Target ist eine colEdge in einem anderen Target zugeordnet
    // Alle RayEdges durchgehen und schauen, ob eine mit einem colEdge eines matchingTargets verbunden ist
    connectedTarget = null;
    foreach (GameObject rayEdge in rayEdges)
    {
      actualEdge = rayEdge;
      if (rayEdge.GetComponent<RayEdge>().isConnected())
      {
        connectedTarget = rayEdge.GetComponent<RayEdge>().getMatchingColEdge().transform.parent.parent.gameObject;
        // Debug.Log(gameObject.name + " meldet, dass eine Edge mit " + connectedTarget.name + " verbunden wurde");
        // Nur eine rayEdge prüfen, reicht in den meisten Fällen und dauert nicht so lange
        targetConnected = true;
        break;
      }
      else targetConnected = false;
    }

    if (targetConnected && rayEdges.Count > 0)
    {
			gameObject.transform.localEulerAngles = new Vector3(0,0,0);
			gameObject.transform.localPosition = new Vector3(0,0,0);
      List<GameObject> toSwitchParentGameObjects = new List<GameObject>();
      foreach (Transform child in connectedTarget.transform)
      {
        toSwitchParentGameObjects.Add(child.gameObject);
      }

      foreach (GameObject child in toSwitchParentGameObjects)
      {
        switchParentTarget(child);
      }
      actualEdge.GetComponent<RayEdge>().getMatchingColEdge().SetActive(true);
   
      Destroy(actualEdge);

       setEdges();
       rearrangeOwnChildren();
      GetComponent<AudioSource>().Play();
    }
  }

  // Nach dem Zusammenführen zweier targets müssen die Edges neu zugeordnet werden!!!
  public void setEdges()
  {

    // Wenn es mehr als einen "Raycasters" bzw "Colliders" Ordner gibt, werden die einzelnen Edges in den jeweils ersten gesteckt.
    int colcount = 0, raycount = 0;
    Transform firstCol = null, firstRay = null;
    rayEdges.Clear();
    colEdges.Clear();
    foreach (Transform child in transform)
    {
      if (child.tag == "Colliders")
      {
        colcount++;
        if (colcount == 1)
        {
          firstCol = child;
        }
        else if (colcount > 1)
        {
          foreach (Transform grandchild in child)
          {
            grandchild.parent = firstCol;
          }
          Destroy(child.gameObject);
        }
      }
      if (child.tag == "Raycasters")
      {
        raycount++;
        if (raycount == 1)
        {
          firstRay = child;
        }
        else if (raycount > 1)
        {
          foreach (Transform grandchild in child)
          {
            grandchild.parent = firstRay;
          }
          Destroy(child.gameObject);
        }
      }
    }

    // GameObjekte in Arrays packen, um mit ihnen weiter arbeiten zu können
    foreach (Transform child in transform)
    {
      if (child.tag == "Colliders")
      {
        foreach (Transform grandchild in child.transform)
        {
          if (grandchild.tag == "ColEdge")
          {
            colEdges.Add(grandchild.gameObject);
          }
        }
      }
      else if (child.tag == "Raycasters")
      {
        foreach (Transform grandchild in child.transform)
        {
          if (grandchild.tag == "RayEdge")
          {
            rayEdges.Add(grandchild.gameObject);
          }
        }
      }
    }
  }

  public List<GameObject> getColEdges()
  {
    return colEdges;
  }

  public List<GameObject> getRayEdges()
  {
    return rayEdges;
  }

  public bool targetIsConnected()
  {
    return targetConnected;
  }

  public GameObject getConnectedTarget()
  {
    return connectedTarget;
  }

  public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
  {
    if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED)
    {
      OnTrackingFound();
    }
    else
    {
      OnTrackingLost();
    }
  }

  public void switchParentTarget(GameObject child)
  {
    // Debug.Log("Erkannte Children: " + child.name);
    if (child.tag == "Colliders")
    {
      child.transform.parent = transform;
      child.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    else if (child.tag == "Raycasters")
    {
      child.transform.parent = transform;
      child.transform.localEulerAngles =  new Vector3(0, 0, 0);
    }
    else if (child.tag == "Model")
    {

      // Model als Child Objekt des aktuellen Image Targets setzen
      // Für jedes Target "setEdges neu setzen
      // Collider und Raycaster in die vorgesehenen Unterordner einsortieren 
      child.transform.parent = transform;
      child.transform.localEulerAngles =  new Vector3(0, 0, 0);
      child.GetComponent<MeshRenderer>().enabled = true;
			

    }
    rearrangeOwnChildren();

  }

  public void rearrangeOwnChildren()
  {
    GameObject centermodel = null;
    switch (gameObject.name)
    {
      case "ImageTarget1": centermodel = GameObject.Find("Model1"); break;
      case "ImageTarget2": centermodel = GameObject.Find("Model2"); break;
      case "ImageTarget3": centermodel = GameObject.Find("Model3"); break;
      case "ImageTarget4": centermodel = GameObject.Find("Model4"); break;
      case "ImageTarget5": centermodel = GameObject.Find("Model5"); break;
      case "ImageTarget6": centermodel = GameObject.Find("Model6"); break;
      case "ImageTarget7": centermodel = GameObject.Find("Model7"); break;
      case "ImageTarget8": centermodel = GameObject.Find("Model8"); break;
      case "ImageTarget9": centermodel = GameObject.Find("Model9"); break;
    }
    

		Dictionary <string, string[]> modelEdgesRelationships = new Dictionary<string, string[]>();
		modelEdgesRelationships.Add("Model1", new string[]{"RayEdge1", "RayEdge2"});
		modelEdgesRelationships.Add("Model2", new string[]{"ColEdge1", "ColEdge2", "ColEdge3"});
		modelEdgesRelationships.Add("Model3", new string[]{"RayEdge3", "RayEdge4"});
		modelEdgesRelationships.Add("Model4", new string[]{"ColEdge4", "ColEdge5", "ColEdge6"});
		modelEdgesRelationships.Add("Model5", new string[]{"RayEdge5", "RayEdge6", "RayEdge7", "RayEdge8"});
		modelEdgesRelationships.Add("Model6", new string[]{"ColEdge7", "ColEdge8", "ColEdge9"});
		modelEdgesRelationships.Add("Model7", new string[]{"RayEdge9", "RayEdge10"});
		modelEdgesRelationships.Add("Model8", new string[]{"ColEdge10", "ColEdge11", "ColEdge12"});
		modelEdgesRelationships.Add("Model9", new string[]{"RayEdge11", "RayEdge12"});
		
		foreach(Transform child in transform){
			if(child.tag == "Model"){
        child.transform.localEulerAngles = new Vector3(0,rotationY,0);
				foreach(string edgename in modelEdgesRelationships[child.name]){
					if(edgename[0] == 'R' && GameObject.Find(edgename) != null ){
						GameObject.Find(edgename).GetComponent<RayEdge>().setNewPosition(defaultPosition, new Vector3(0, rotationY, 0));
					}
					else if (edgename[0] == 'C' && GameObject.Find(edgename) != null){
						GameObject.Find(edgename).GetComponent<ColEdge>().setNewPosition(defaultPosition, new Vector3(0, rotationY, 0));
					}
				}
        if(rotationY == 0){
          switch (child.name)
           {
            case "Model1":
              child.transform.localPosition = new Vector3(0, 0, 0) - defaultPosition ;break;
            case "Model2":
              child.transform.localPosition = new Vector3(1, 0, 0) - defaultPosition; break;
            case "Model3":
              child.transform.localPosition = new Vector3(2, 0, 0) - defaultPosition; break;
            case "Model4":
              child.transform.localPosition = new Vector3(0, 0, 1) - defaultPosition; break;
            case "Model5":
              child.transform.localPosition = new Vector3(1, 0, 1) - defaultPosition; break;
            case "Model6":
              child.transform.localPosition = new Vector3(2, 0, 1) - defaultPosition; break;
            case "Model7":
              child.transform.localPosition = new Vector3(0, 0, 2) - defaultPosition; break;
            case "Model8":
              child.transform.localPosition = new Vector3(1, 0, 2) - defaultPosition; break;
            case "Model9":
              child.transform.localPosition = new Vector3(2, 0, 2) - defaultPosition; break;
          }
        }
				else if(rotationY == 270){
          switch (child.name)
           {
            case "Model1":
              child.transform.localPosition = new Vector3(2, 0, 0) - defaultPosition ;break;
            case "Model2":
              child.transform.localPosition = new Vector3(2, 0, 1) - defaultPosition; break;
            case "Model3":
              child.transform.localPosition = new Vector3(2, 0, 2) - defaultPosition; break;
            case "Model4":
              child.transform.localPosition = new Vector3(1, 0, 0) - defaultPosition; break;
            case "Model5":
              child.transform.localPosition = new Vector3(1, 0, 1) - defaultPosition; break;
            case "Model6":
              child.transform.localPosition = new Vector3(1, 0, 2) - defaultPosition; break;
            case "Model7":
              child.transform.localPosition = new Vector3(0, 0, 0) - defaultPosition; break;
            case "Model8":
              child.transform.localPosition = new Vector3(0, 0, 1) - defaultPosition; break;
            case "Model9":
              child.transform.localPosition = new Vector3(0, 0, 2) - defaultPosition; break;
          }
        }
        else if(rotationY == 180){
          switch (child.name)
           {
            case "Model1":
              child.transform.localPosition = new Vector3(2, 0, 2) - defaultPosition ;break;
            case "Model2":
              child.transform.localPosition = new Vector3(1, 0, 2) - defaultPosition; break;
            case "Model3":
              child.transform.localPosition = new Vector3(0, 0, 2) - defaultPosition; break;
            case "Model4":
              child.transform.localPosition = new Vector3(2, 0, 1) - defaultPosition; break;
            case "Model5":
              child.transform.localPosition = new Vector3(1, 0, 1) - defaultPosition; break;
            case "Model6":
              child.transform.localPosition = new Vector3(0, 0, 1) - defaultPosition; break;
            case "Model7":
              child.transform.localPosition = new Vector3(2, 0, 0) - defaultPosition; break;
            case "Model8":
              child.transform.localPosition = new Vector3(1, 0, 0) - defaultPosition; break;
            case "Model9":
              child.transform.localPosition = new Vector3(0, 0, 0) - defaultPosition; break;
          }
        }
        else if(rotationY == 90){
          switch (child.name)
           {
            case "Model1":
              child.transform.localPosition = new Vector3(0, 0, 2) - defaultPosition ;break;
            case "Model2":
              child.transform.localPosition = new Vector3(0, 0, 1) - defaultPosition; break;
            case "Model3":
              child.transform.localPosition = new Vector3(0, 0, 0) - defaultPosition; break;
            case "Model4":
              child.transform.localPosition = new Vector3(1, 0, 2) - defaultPosition; break;
            case "Model5":
              child.transform.localPosition = new Vector3(1, 0, 1) - defaultPosition; break;
            case "Model6":
              child.transform.localPosition = new Vector3(1, 0, 0) - defaultPosition; break;
            case "Model7":
              child.transform.localPosition = new Vector3(2, 0, 2) - defaultPosition; break;
            case "Model8":
              child.transform.localPosition = new Vector3(2, 0, 1) - defaultPosition; break;
            case "Model9":
              child.transform.localPosition = new Vector3(2, 0, 0) - defaultPosition; break;
          }
        }
			}
      else if(child.tag == "Raycasters" || child.tag == "Colliders"){
        child.transform.localEulerAngles = new Vector3(0,0,0);
        
      }
		}
  }
  public void OnTrackingFound()
  {
    gameObject.GetComponent<Target>().enabled = true;
    // setEdges();
    // rearrangeOwnChildren();
  }

  public void OnTrackingLost()
  {
    gameObject.GetComponent<Target>().enabled = false;
  }

  public void rotateTarget(){
    if(rotationY <= 180){
      rotationY += 90;
    } else {
      rotationY = 0;
    }

    if(rotationY == 0){
      switch(gameObject.name){
        case "ImageTarget1" : defaultPosition = new Vector3(0,0,0); break;
        case "ImageTarget2" : defaultPosition = new Vector3(1,0,0); break;
        case "ImageTarget3" : defaultPosition = new Vector3(2,0,0); break;
        case "ImageTarget4" : defaultPosition = new Vector3(0,0,1); break;
        case "ImageTarget5" : defaultPosition = new Vector3(1,0,1); break;
        case "ImageTarget6" : defaultPosition = new Vector3(2,0,1); break;
        case "ImageTarget7" : defaultPosition = new Vector3(0,0,2); break;
        case "ImageTarget8" : defaultPosition = new Vector3(1,0,2); break;
        case "ImageTarget9" : defaultPosition = new Vector3(2,0,2); break;
      }
    } else if (rotationY == 270){
      switch(gameObject.name){
        case "ImageTarget1" : defaultPosition = new Vector3(2,0,0); break;
        case "ImageTarget2" : defaultPosition = new Vector3(2,0,1); break;
        case "ImageTarget3" : defaultPosition = new Vector3(2,0,2); break;
        case "ImageTarget4" : defaultPosition = new Vector3(1,0,0); break;
        case "ImageTarget5" : defaultPosition = new Vector3(1,0,1); break;
        case "ImageTarget6" : defaultPosition = new Vector3(1,0,2); break;
        case "ImageTarget7" : defaultPosition = new Vector3(0,0,0); break;
        case "ImageTarget8" : defaultPosition = new Vector3(0,0,1); break;
        case "ImageTarget9" : defaultPosition = new Vector3(0,0,2); break;
      }
    } else if (rotationY == 180){
      switch(gameObject.name){
        case "ImageTarget1" : defaultPosition = new Vector3(2,0,2); break;
        case "ImageTarget2" : defaultPosition = new Vector3(1,0,2); break;
        case "ImageTarget3" : defaultPosition = new Vector3(0,0,2); break;
        case "ImageTarget4" : defaultPosition = new Vector3(2,0,1); break;
        case "ImageTarget5" : defaultPosition = new Vector3(1,0,1); break;
        case "ImageTarget6" : defaultPosition = new Vector3(0,0,1); break;
        case "ImageTarget7" : defaultPosition = new Vector3(2,0,0); break;
        case "ImageTarget8" : defaultPosition = new Vector3(1,0,0); break;
        case "ImageTarget9" : defaultPosition = new Vector3(0,0,0); break;
      }
    } else if (rotationY == 90){
      switch(gameObject.name){
        case "ImageTarget1" : defaultPosition = new Vector3(0,0,2); break;
        case "ImageTarget2" : defaultPosition = new Vector3(0,0,1); break;
        case "ImageTarget3" : defaultPosition = new Vector3(0,0,0); break;
        case "ImageTarget4" : defaultPosition = new Vector3(1,0,2); break;
        case "ImageTarget5" : defaultPosition = new Vector3(1,0,1); break;
        case "ImageTarget6" : defaultPosition = new Vector3(1,0,0); break;
        case "ImageTarget7" : defaultPosition = new Vector3(2,0,2); break;
        case "ImageTarget8" : defaultPosition = new Vector3(2,0,1); break;
        case "ImageTarget9" : defaultPosition = new Vector3(2,0,0); break;
      }
    }
    foreach(Transform child in transform){
      child.RotateAround(transform.localPosition, transform.up, 90);
    }
  }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterMovement : MonoBehaviour
{
    // Adujustable Raycast Modifier
    public float moveSpeedMultiplicator;
    public float moveGroundSpeed;
    public float distanceMultiplicator;
	public GameObject drawing;

    // Raycast length
    private float distanceToTheRight = 0.04f;
    public float distanceToWall = 0.01f;
    public float distanceToFloor = 0.5f;

    // Movement Variables + Animation
    private bool isWalking, isMovingUp = false, isMovingDown = false;
    private float angleDistance;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Animator m_Anim;

    private Rigidbody2D rBody;
    public GameObject obstacleCollider;

	//Menu
	GameObject [] menuObjects;
	GameObject [] resetObjects;
	//GameObject deleteDrawing;

    // Chest
    public SpriteRenderer chestColliderWithSprite;
    public Sprite openChestSprite;
	public Sprite closedChestSprite;
	public float waitToShowSprite = 2.5f;

    // MoveSight variables
    private GameObject[] moveFunctionTrigger;
    private GameObject toMove;
	public GameObject drawingObj;
	private Vector3 posFigur;
	private Vector3 posLevel;

	private Vector3 lastObstaclePos;

	private string moveName;

	private int collided = 0;

    //Distanz um die das Level verschoben werden soll
    public float slideToLeftValue = 0.0036f;
    private bool initiated = false;

    public bool debug = false;
    private float lineDifference;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();

        if (distanceMultiplicator == 0)
        {
            Debug.LogWarning("distanceMultiplicator: not assigned, using default value.");
            distanceMultiplicator = 1.0f;
        }

        if (moveGroundSpeed == 0)
        {
            Debug.LogWarning("moveGroundSpeed: not assigned, using default value.");
            moveGroundSpeed = 0.04f;
        }

        if (moveSpeedMultiplicator == 0)
        {
            Debug.LogWarning("moveSpeedMultiplicator: not assigned, using default value.");
            moveSpeedMultiplicator = 10;
        }

        if (distanceToFloor == 0)
        {
            Debug.LogWarning("distanceToFloor: not assigned, using default value.");
            distanceToFloor = 0.05f;
        }

        if (distanceToWall == 0)
        {
            Debug.LogWarning("distanceToWall: not assigned, using default value.");
            distanceToWall = 0.01f;
        }

		//Startmenu öffnen
		menuObjects = GameObject.FindGameObjectsWithTag ("Menu");
		resetObjects = GameObject.FindGameObjectsWithTag ("Reset");
		toMove = GameObject.FindGameObjectWithTag("Level");
		moveFunctionTrigger = GameObject.FindGameObjectsWithTag("MoveSight");
		//deleteDrawing = GameObject.FindGameObjectWithTag ("Drawing");



		//ursprüngliche Position der Figur sichern
		posFigur = transform.position;
		posLevel = toMove.transform.position;

		goToStart ();

        initiated = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (initiated)
        {
            m_Anim.SetBool("Ground", true);
            canMove();

            if (isWalking && isMovingUp)
            {
                m_Anim.SetFloat("Speed", 0.5f);
                moveUp(moveGroundSpeed, lineDifference);
            }
            else if (isWalking && isMovingDown)
            {
                m_Anim.SetFloat("Speed", 0.5f);
                moveDown(moveGroundSpeed, lineDifference);
            }
            else if (isWalking)
            {
                m_Anim.SetFloat("Speed", 0.5f);
                Move(moveGroundSpeed);
            }
            else
            { 
                m_Anim.SetFloat("Speed", 0);
            }
        }
    }

   
    // Checks if the Player can move by checking for a collider, sets isWalking to either true or false
	public void canMove()
    {
        RaycastHit2D[] lines;
        Vector2 currentPos;
        Vector2 startPos;
		Vector2 posDown2;

        /*
        currentPos = transform.position;

        posToCheck = transform.position;
        posToCheck.x = currentPos.x + distanceToTheRight * distanceMultiplicator;

        posDown2 = currentPos;
        posDown2.y = posDown2.y - distanceToFloor;
        */
        // Dude, make sure to use the right thing to test shit... Ray =/= Line
        GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");

        currentPos = groundCheck.transform.position;

        startPos = currentPos;
        startPos.x = currentPos.x + distanceToTheRight * distanceMultiplicator;

        posDown2 = currentPos;
        posDown2.y = posDown2.y - distanceToFloor;

        Debug.DrawLine(startPos, posDown2, Color.blue); // Down

        //Debug.DrawLine(posToCheck, posDown2, Color.blue); // Down
        // Debug.DrawRay(posToCheck, posDown2, Color.red);
        //Debug.DrawRay(posToCheck, Vector2.down, Color.yellow); // Down

        // Checks if the player is grounded
        lines = Physics2D.LinecastAll(startPos, posDown2);
        //hits = Physics2D.RaycastAll(posToCheck, posDown2); //posDown

        isWalking = false;
        isMovingUp = false;
        isMovingDown = false;

        /*
        foreach (RaycastHit2D line in lines)
        {
            Debug.Log("Down hits: " + line.transform.gameObject.name + "\n\tTag: " + line.collider.tag);

		if(line.collider.tag == "obstacle") { // dont forget to add tag to brush ???
				isWalking = true;
				Debug.Log ("isWalking: " + isWalking);
			}
        }*/

        foreach (RaycastHit2D line in lines)
        {
            if (debug)
            {
                Debug.Log(""
                    //"Down hits: " + line.transform.gameObject.name
                //+ "\n\tTag: " + line.collider.tag
                //+ "\n\tDistance to Line: " + line.distance
                //+ "\tFraction: " + line.fraction
                //+ "\tPoint: " + line.point
                +"");
            }


            if (line.collider.tag == "obstacle")
            { // dont forget to add tag to brush ???
                isWalking = true;

                Vector2 posAngleUp = currentPos;
                posAngleUp.x = posAngleUp.x + 0.02f;
                posAngleUp.y = posAngleUp.y - 0.06f;

                Debug.DrawLine(currentPos, posAngleUp, Color.green); // Angle

                Vector2 posAngleDown = currentPos;
                posAngleDown.x = posAngleDown.x + 0.01f;
                posAngleDown.y = posAngleDown.y - 0.02f;

                Debug.DrawLine(currentPos, posAngleDown, Color.cyan); // Angle
                
                //Physics.SphereCastAll

                RaycastHit2D[] angleHits = Physics2D.LinecastAll(currentPos, posAngleUp);

                foreach (RaycastHit2D angle in angleHits)
                {
                    if(angle.collider.tag == "obstacle")
                    {
                        angleDistance = angle.distance;
                        float angleFraction = angle.fraction;

                        /*
                    
                        Debug.Log("<color=red>Angle:" + angle.fraction + "</color>"
                        + "<color=blue>Line:" + line.fraction + "</color>");
                        */

                        // normal: 0.15 - 0.13
                        // down 0.15-0.19
                        if (angleFraction >= 0.13)
                        {
                            // up
                        }else if(angleFraction <= 0.12)
                        {
                            // down
                        }

                        //isWalking = true;
                        if (angleDistance < line.distance)
                        { // 3 zu 1
                            isMovingUp = true;
                            lineDifference = angleDistance - line.distance;


                        }else if (angleDistance > line.distance) { 
                        }
                                              

                        //oldAngleDistance = angle.distance;
                        //Debug.Log("<color=red>Angle :" + lineDifference + "</color>");
                    }
                }

                 

                // xxxxxxxxxxxxxxxxxxxx
                /*
                if (line.distance > 0.0048f)
                {
                    
                    
                } 
                else if (line.distance < 0.0048f)
                {
                    isMovingDown = true;
                    lineDistance = line.distance;
                }*/

                if (debug)
                {
                    Debug.Log("isWalking: " + isWalking);
                }
            }
        }


        // posToCheck.y -= 0.003f; // lowers the wallChecker
        startPos.y += 0.009f; // lowers the wallChecker


        Vector2 wallDistance = startPos + new Vector2(distanceToWall, 0);
        Debug.DrawLine(startPos, wallDistance, Color.red);
        // Checks if the player reached a wall	

        RaycastHit2D[] wallHits = Physics2D.LinecastAll(startPos, wallDistance);
        //RaycastHit2D hit2 = (Physics2D.Linecast(posToCheck, wallDistance));
        foreach (RaycastHit2D hit2 in wallHits)
        {
            if (debug)
            {
                 Debug.Log("Right hits: " + hit2.collider.name);
            }

            if (hit2.collider.tag == "obstacle")
            {
                isWalking = false;
                //isMovingUp = false;
            }
			if (hit2.collider.tag == "Treasure") {
				isWalking = false;
				Debug.Log("Ende erreicht.");
				//Ende des Spiels, Öffnen der Schatztruhe, wi, Neustart nach 1 min
				chestColliderWithSprite.sprite = openChestSprite;
				//float waitStart = Time.time;
				//if ((Time.time - waitStart) > 5f) {
					//StartCoroutine(waiting ());
					//Anfangsszene aufrufen

					waitToShowSprite -= Time.deltaTime;
					Debug.Log("seconds left: " + waitToShowSprite);
					if (waitToShowSprite < 0f) {
						resetTheGame();
						goToStart();
					}
				//}
			}
			if (hit2.collider.tag == "MoveSight") {
				collided++;
				Debug.Log("Movement Trigger berührt. Setze zurück.");
				//position auf dem obstacle speichern, damit man dorthin zurück kann
				lastObstaclePos = transform.position;
				//figur verschieben
				transform.position -= new Vector3(slideToLeftValue, 0.0f, 0.0f);
				//level verschieben
				toMove.transform.position -= new Vector3(slideToLeftValue, 0.0f, 0.0f);
				//hochzählen auf welchem Auslöser man zuletzt stand, damit man dorthin zurück kann bei resetDrawings
				//diesen MoveSight ausschalten
				moveName = gameObject.transform.name;
			}
			if (hit2.collider.tag == "border")
			{
				Debug.Log("Border! Starte Szene neu.");
				resetTheGame ();
			}
        }

        Debug.Log("isWalking: " + isWalking);

    }

    // Moves the Character
    public void Move(float move)
    {
        //Debug.Log("Move Called");
        //m_Anim.SetFloat("Speed", Mathf.Abs(move));
        //m_Anim.SetBool("Ground", false);
        
        //transform.position += Vector3.forward * move* moveSpeedMultiplicator;
        //transform.position.y += new Vector2(move * moveSpeedMultiplicator, 0);
        //transform.position += Vector3.forward * Time.deltaTime * move;

        Vector2 currentPosition = rBody.position;

        currentPosition.x += move * moveSpeedMultiplicator;
        rBody.MovePosition(currentPosition);

        //rBody.velocity = new Vector2(move * moveSpeedMultiplicator, 0);  // Moves the character
        
        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    // Moves the Character
    public void moveUp(float move, float distanceToLine)
    {
        //Debug.Log("<color=red>MoveUp Called</color>");

        Vector2 currentPosition = rBody.position;

        currentPosition.x += move * moveSpeedMultiplicator;
        currentPosition.y += angleDistance * moveSpeedMultiplicator;
        rBody.MovePosition(currentPosition);

        //rBody.velocity = new Vector2(move * moveSpeedMultiplicator, 0);  // Moves the character

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    // Moves the Character
    public void moveDown(float move, float distanceToLine)
    {
        //Debug.Log("<color=blue>MoveDown Called</color>");

        Vector2 currentPosition = rBody.position;

        currentPosition.x += move * moveSpeedMultiplicator;
        currentPosition.y -= angleDistance * moveSpeedMultiplicator;
        rBody.MovePosition(currentPosition);

        //rBody.velocity = new Vector2(move * moveSpeedMultiplicator, 0);  // Moves the character

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	//onclick von resetButton
	public void resetDrawings(){ 
		//moveName
		GameObject.Find(moveName).SetActive(false);
		//setzt Figur auf letztes Obstacle
		SetFigurToLastObstacle ();
		//delete the drawings
		DeleteDrawings ();
		RestoreDrawingObj ();
	}


	private void RestoreDrawingObj(){
		GameObject obj = Instantiate (drawingObj, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
	}

	private void DeleteDrawings(){
		Debug.Log ("Destroy Drawings!");
		Destroy (drawing);
	}


	private void SetFigurToLastObstacle(){
		//position of movesight --> movesight löschen?
		if (collided != 0) {
			Debug.Log ("Set Figur to last obstacle!");
			transform.position = lastObstaclePos;
		}
	}


	//versteckt das menu und startet das spiel
	public void startTheGame()
	{
		//Spiel zurücksetzen
		//Menu weg
		hideMenu ();
		showReset ();
		//Pause aufheben
		Time.timeScale = 1;
	}

	//pausiert den Bildschirm und öffnet den menu canvas
	public void goToStart(){
			Time.timeScale = 0;
			hideReset ();
			showMenu ();
	}

	//setzt die Levelelemente und die Figur wieder auf ihre Anfangsposition
	public void resetTheGame(){
		DeleteDrawings ();
		//level & Figur zurück an Anfangsposition (3 mal zurück)
		transform.position = posFigur;
		toMove.transform.position = posLevel;
		//sprite für treasure zurücksetzen
		chestColliderWithSprite.sprite = closedChestSprite;
	}

	//zeigt das menu
	public void showMenu(){
		foreach (GameObject m in menuObjects) {
			m.SetActive (true);
		}
	}

	//versteckt das menu
	public void hideMenu(){
		foreach (GameObject m in menuObjects) {
			m.SetActive (false);
		}
	}

	public void showReset(){
		foreach (GameObject r in resetObjects) {
			r.SetActive(true);
		}
	}

	public void hideReset(){
		foreach (GameObject r in resetObjects) {
			r.SetActive (false);
		}
	}
		
	IEnumerator waiting(){
		print ("time nr 1:" + Time.time);
		yield return new WaitForSeconds(2);
		//Anfangsszene aufrufen
		resetTheGame();
		goToStart();
		print ("time nr 2:" + Time.time);
	}

}

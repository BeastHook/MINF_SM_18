using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterMovementMixUp : MonoBehaviour
{
	// Adujustable Raycast Modifier
	public float moveSpeedMultiplicator;
	public float moveGroundSpeed;
	public float distanceMultiplicator;

	// Raycast length
	private float distanceToTheRight = 0.04f;
	private float distanceToWall = 0.01f;
	public float distanceToFloor = 0.5f;

	// Movement Variables + Animation
	private bool isWalking;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Animator m_Anim;

	private Rigidbody2D rBody;

	public GameObject obstacleCollider;


	// ???
	private GameObject[] levelTrigger;
	private GameObject toMove;

	//
	public float slideToLeftValue = 2.0f;

	//private string bodenName;

	// Chest
	public SpriteRenderer chestColliderWithSprite;
	public Sprite openChestSprite;

	// General Settings
	public float timeTilReset = 240.0f; //4min

	void Start()
	{
		rBody = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator>();
		//chestColliderWithSprite = GetComponent<SpriteRenderer> ();
	}

	void Awake()
	{
		/*
		levels = GameObject.FindGameObjectsWithTag("Level").OrderBy(go=>go.name).ToArray();
		foreach (GameObject level in levels) {
			Debug.Log (level.name);
		}
		*/

		toMove = GameObject.FindGameObjectWithTag("Level");
		Debug.Log ("ToMove: "+toMove.name);

		levelTrigger = GameObject.FindGameObjectsWithTag ("MoveSight");
		rBody = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator>();

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

		if (distanceToFloor == 0) {
			Debug.LogWarning ("rayDistanceDown: not assigned, using default value.");
			distanceToFloor = 0.05f;
		}
	}

	// Update is called once per frame
	void Update()
	{

		run();

		m_Anim.SetBool("Ground", isWalking);
		//m_Anim.SetFloat("vSpeed", rBody.velocity.y);

		// Reset funktion
		timeTilReset -= Time.deltaTime;
		if (timeTilReset <= 0.0f) {
			restartTheScene();
		}

	}

	// ???
	void OnCollisionEnter2D(Collision2D coll)
	{
		//wenn Trigger erreicht wird --> alles mit Tag 'Level' und 'MoveSight' nach links bewegen
		if (coll.gameObject.tag == "MoveSight") {
			this.transform.position -= new Vector3 (slideToLeftValue, 0.0f, 0.0f);
			toMove.transform.position -= new Vector3 (slideToLeftValue, 0.0f, 0.0f);

			foreach (GameObject levelTrig in levelTrigger) {
				levelTrig.transform.position -= new Vector3 (slideToLeftValue, 0.0f, 0.0f);
			}
		} else if (coll.gameObject.tag == "Treasure") {
			//Ende des Spiels, Öffnen der Schatztruhe, wi, Neustart nach 1 min
			chestColliderWithSprite.sprite = openChestSprite;
			//nach Timer2
			//restartTheScene ();
		} else if (coll.gameObject.name == "boden") {
			restartTheScene ();
		}
	}

	void run()
	{
		canMove();

		if (isWalking)
		{
			Move(moveGroundSpeed);
		}
	}

	// Checks if the Player can move by checking for a collider, sets isWalking to either true or false
	public void canMove()
	{
		RaycastHit2D [] hits;
		RaycastHit2D hit; //oder Raycast
		Vector2 currentPosition;
		Vector2 posToCheck;
		Vector3 currentPositionV3 = transform.position;

		currentPosition = transform.position;

		posToCheck = transform.position;
		//posToCheck.x = currentPosition.x + distanceToTheRight * distanceMultiplicator;
		posToCheck.x = currentPosition.x + distanceToTheRight;

		// ???
		Vector3 posDown = currentPositionV3;
		posDown.y = posDown.y - 0.05f;


		Debug.DrawRay(posToCheck, Vector2.down, Color.yellow); // Shows the Ray in Yellow
		Debug.DrawLine(posToCheck, posDown, Color.green);
		//Debug.DrawLine(posToCheck, Vector2.down, Color.white);

		//Vector2 endPoint = posToCheck + new Vector2 (distanceToFloor * distanceMultiplicator, 0);


		Vector2 downPoint = posToCheck + new Vector2 (0, distanceToFloor);

		//Debug.DrawRay(downPoint, Vector2.down, Color.green); 

		// Checks if the player is grounded
		hits = Physics2D.RaycastAll(posToCheck, downPoint);
		for(int i = 0; i < hits.Length; i++){
			hit = hits [i]; // (Physics2D.Raycast(posToCheck, downPoint));
			if (hit) {
				Debug.Log ("Yellow hits: " + hit.transform.gameObject.name);
				if (hit.collider.name == obstacleCollider.name) {
					//|| hit.collider.name == "hintergrund" || hit.collider.name == "Stift_white(Clone)") // returns a RaycastHit2D 
					isWalking = true;

					Vector2 endPoint = posToCheck + new Vector2 (distanceToWall, 0);

					Debug.DrawLine (posToCheck, endPoint, Color.red);
					// Checks if the player reached a wall	
					RaycastHit2D hit2 = (Physics2D.Linecast (posToCheck, endPoint));

					if (hit2) {
						Debug.Log ("Red hits: " + hit2.collider.name);

						if (hit2.collider.name == this.gameObject.name) {
							isWalking = true;
						} else {
							isWalking = false;
						}
					} else {
						isWalking = false;
					}
				} else {
					isWalking = false;
				}
			}
		}
	}


	// Moves the Character
	public void Move(float move)
	{

		m_Anim.SetFloat("Speed", Mathf.Abs(move));


		// rBody.velocity.y
		rBody.velocity = new Vector2(move * moveSpeedMultiplicator, 0);  // Moves the character
		//transform.position = new Vector2(move*moveSpeed, transform.position.y);


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

	public void restartTheScene(){
		//timer --> dann neustart der Scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

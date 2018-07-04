using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovementOld : MonoBehaviour
{
    public float moveSpeedMultiplicator;
    public float moveGroundSpeed;
    public float distanceMultiplicator;

    private float distanceToTheRight = 0.04f;
    private float distanceToWall = 0.01f;

    private bool isWalking;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Animator m_Anim;
    private Rigidbody2D rBody;

	private GameObject[] levels;
	private GameObject[] levelTrigger;
	public float slide = 2.0f;
	public GameObject bodenColl;
	private string bodenName;

    // Chest
    public SpriteRenderer rendererTreasure;
    public Sprite newSprite;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }

    void Awake()
    {
		bodenName = bodenColl.name;
		levels = GameObject.FindGameObjectsWithTag("Level");
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

     }

    // Update is called once per frame
    void Update()
    {

        run();

        m_Anim.SetBool("Ground", isWalking);
        //m_Anim.SetFloat("vSpeed", rBody.velocity.y);
    }

	void OnCollisionEnter2D(Collision2D coll)
	{
		//wenn Trigger erreicht wird --> alles mit Tag 'Level' und 'MoveSight' nach links bewegen
		if (coll.gameObject.tag == "MoveSight") {
			this.transform.position -= new Vector3 (slide, 0.0f, 0.0f);
			foreach (GameObject level in levels) {
				level.transform.position -= new Vector3 (slide, 0.0f, 0.0f);
			}
			foreach (GameObject levelTrig in levelTrigger) {
				levelTrig.transform.position -= new Vector3 (slide, 0.0f, 0.0f);
			}
		}
		else if(coll.gameObject.tag == "Treasure"){
			//Ende des Spiels, Öffnen der Schatztruhe, wi, Neustart nach 1 min
			rendererTreasure.sprite = newSprite;
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

        //Debug.Log("Old "+currentPos);
        //currentPos.y = currentPos.y - 0.5f;
        //Debug.Log("New "+currentPos);

    }

    // Checks if the Player can move by checking for a collider, sets isWalking to either true or false
    public void canMove()
    {
        RaycastHit2D hit;
        Vector2 currentPos;
        Vector2 posToCheck;

        Vector3 currentPos3 = transform.position;

        currentPos = transform.position;

        posToCheck = transform.position;
        posToCheck.x = currentPos.x + distanceToTheRight * distanceMultiplicator;

        Vector3 posDown = currentPos3;
        posDown.y = posDown.y - 0.05f;


		Debug.DrawRay(posToCheck, Vector2.down, Color.yellow); // Shows the Ray in Yellow
        Debug.DrawLine(posToCheck, posDown, Color.green);
        //Debug.DrawLine(posToCheck, Vector2.down, Color.white);

        // Checks if the player is grounded
        hit = (Physics2D.Raycast(posToCheck, Vector2.down));
        if (hit)
        {
            Debug.Log("Down hits: " + hit.transform.gameObject.name);
			if (hit.collider.name == bodenName)
				//|| hit.collider.name == "hintergrund" || hit.collider.name == "Stift_white(Clone)") // returns a RaycastHit2D 
            {
                isWalking = true;

                Vector2 endPoint = posToCheck + new Vector2(distanceToWall * distanceMultiplicator, 0);

                Debug.DrawLine(posToCheck, endPoint, Color.red);
                // Checks if the player reached a wall	
                RaycastHit2D hit2 = (Physics2D.Linecast(posToCheck, endPoint));

                if (hit2)
                {
                    Debug.Log("Red hits: " + hit2.collider.name);

                    if (hit2.collider.name == this.gameObject.name)
                    {
                        isWalking = true;
                    }
                    else
                    {
                        isWalking = false;
                    }
                }


            }
            else
            {
                isWalking = false;
            }
        }else
        {
            isWalking = false;
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

	private void restartTheScene(){
		//timer --> dann neustart der Scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

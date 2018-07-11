using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterMovementWithSlopesv1 : MonoBehaviour
{
    public float minGroundNormalY = 1.12f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rBody;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.004f;
    protected const float shellRadius = 0.035f;

    // Adujustable Raycast Modifier
    public float moveSpeedMultiplicator;
    public float moveGroundSpeed;
    public float distanceMultiplicator;

    // Raycast length
    private float distanceToTheRight = 0.85f;
    public float distanceToWall = 0.01f;
    public float distanceToFloor = 0.5f;
    private float lineDifference;

    // Movement Variables + Animation
    private bool isWalking, isMovingUp = false, isMovingDown = false;
    private float angleDistance;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Animator m_Anim;

    public bool debug = false;
    private bool initiated = false;

    // später private machen
    public RaycastHit2D hitCollidedWith;
    private Vector2 desiredVelocity;

    void Start()
    {
        rBody = this.GetComponent<Rigidbody2D>();
        m_Anim = this.GetComponent<Animator>();

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

        initiated = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (initiated)
        {
            m_Anim.SetBool("Ground", true);

            //canMove();
            if (isWalking)
            {
                m_Anim.SetFloat("Speed", 0.5f);
                //Move(moveGroundSpeed);
            }
            else
            {
                m_Anim.SetFloat("Speed", 0);
            }

            targetVelocity = Vector2.zero;
            ComputeVelocity();
        }
    }

    protected void ComputeVelocity()
    {
        if (grounded && isWalking)
        {
            m_Anim.SetFloat("Speed", 0.5f);
            targetVelocity.x += moveGroundSpeed;
        }
        else
        {
            m_Anim.SetFloat("Speed", 0);
        }

    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rBody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                if (hitBufferList[i].collider.tag == "obstacle")
                {
                    //isWalking = true;
                }

                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        // hit wall thing einbauen

        Vector2 currentPos;
        Vector2 startingPos;

        GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
        currentPos = groundCheck.transform.position;

        startingPos = currentPos;
        startingPos.x = currentPos.x + distanceToTheRight * distanceMultiplicator;

        startingPos.y += 0.015f;

        Vector2 wallDistance = startingPos + new Vector2(distanceToWall, 0);
        Debug.DrawLine(startingPos, wallDistance, Color.red);
        // Checks if the player reached a wall	

        RaycastHit2D[] wallHits = Physics2D.LinecastAll(startingPos, wallDistance);
        //RaycastHit2D hit2 = (Physics2D.Linecast(posToCheck, wallDistance));
        foreach (RaycastHit2D hit2 in wallHits)
        {
            hitCollidedWith = hit2;

            if (debug)
            {
                Debug.Log("Right hits: " + hit2.collider.name);
            }

            if (hit2.collider.tag == "obstacle")
            {
                isWalking = false;
            }
            if (hit2.collider.tag == "Treasure")
            {
                isWalking = false;
            }
        }

        // Achtung

        RaycastHit2D[] lines;
        Vector2 posDown2;
        startingPos = currentPos;
        startingPos.x = currentPos.x + distanceToTheRight * distanceMultiplicator;
        posDown2 = currentPos;
        posDown2.y = posDown2.y - distanceToFloor;

        //Debug.DrawLine(startingPos, posDown, Color.blue); // Down
        Debug.DrawRay(startingPos, Vector3.down, Color.cyan); // Down

        // Checks if the player is grounded
        //lines = Physics2D.LinecastAll(startingPos, posDown2);
        lines = Physics2D.RaycastAll(startingPos, Vector3.down, distanceToFloor);

        isWalking = false;

        foreach (RaycastHit2D line in lines)
        {
            //hitCollidedWith = line;
            //Debug.Log("Down hits: " + line.transform.gameObject.name + "\n\tTag: " + line.collider.tag);

            if (line.collider.tag == "obstacle")
            {
                isWalking = true;
            }
        }


        // Achtung

        rBody.position = rBody.position + move.normalized * distance;
    }

    // Checks if the Player can move by checking for a collider, sets isWalking to either true or false
    public void canMove()
    {
        RaycastHit2D[] lines;

        Vector2 currentPos;
        Vector2 startingPos;
        Vector2 posDown2;

        GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
        currentPos = groundCheck.transform.position;

        startingPos = currentPos;
        startingPos.x = currentPos.x + distanceToTheRight * distanceMultiplicator;

        posDown2 = currentPos;
        posDown2.y = posDown2.y - distanceToFloor;

        Debug.DrawLine(startingPos, posDown2, Color.blue); // Down

        // Checks if the player is grounded
        lines = Physics2D.LinecastAll(startingPos, posDown2);

        isWalking = false;


        foreach (RaycastHit2D line in lines)
        {
            hitCollidedWith = line;
            Debug.Log("Down hits: " + line.transform.gameObject.name + "\n\tTag: " + line.collider.tag);

            if (line.collider.tag == "obstacle")
            {
                isWalking = true;

                // Testing
                Vector2 currentNormal = line.normal;

                Debug.DrawLine(currentPos, currentNormal, Color.yellow); // Down
                Debug.DrawLine(currentPos, new Vector2(currentNormal.x, minGroundNormalY), Color.green); // Down
                Debug.Log(currentNormal.y + "   " + minGroundNormalY);

                if (currentNormal.y > minGroundNormalY)
                {
                    Debug.Log("Yo");
                }
                // Testing

            }
        }

        startingPos.y += 0.02f;

        Vector2 wallDistance = startingPos + new Vector2(distanceToWall, 0);
        Debug.DrawLine(startingPos, wallDistance, Color.red);
        // Checks if the player reached a wall	

        RaycastHit2D[] wallHits = Physics2D.LinecastAll(startingPos, wallDistance);
        //RaycastHit2D hit2 = (Physics2D.Linecast(posToCheck, wallDistance));
        foreach (RaycastHit2D hit2 in wallHits)
        {
            hitCollidedWith = hit2;

            if (debug)
            {
                Debug.Log("Right hits: " + hit2.collider.name);
            }

            if (hit2.collider.tag == "obstacle")
            {
                isWalking = false;
            }
            if (hit2.collider.tag == "Treasure")
            {
                isWalking = false;
            }
        }
    }

    // Moves the Character
    public void Move(float move)
    {
        Vector2 currentPosition = rBody.position;

        currentPosition.x += move * moveSpeedMultiplicator;
        rBody.MovePosition(currentPosition);

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
    void moveUp(float move, float distanceToLine)
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

}

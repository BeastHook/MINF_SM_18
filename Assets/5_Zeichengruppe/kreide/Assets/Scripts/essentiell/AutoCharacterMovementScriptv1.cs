using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class AutoCharacterMovementScriptv1 : MonoBehaviour
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    //protected const float minMoveDistance = 0.0003f;
    protected const float minMoveDistance = 0.0030f;
    protected const float shellRadius = 0.007f;

    public float distanceMultiplicator;

    // Raycast length
    private float distanceToTheRight = 0.04f;
    public float distanceToWall = 0.01f;
    public float distanceToFloor = 0.5f;
    private float lineDifference;

    // Movement Variables + Animation
    private bool isWalking, isMovingUp = false, isMovingDown = false;
    private float angleDistance;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Animator m_Anim;

    private Rigidbody2D rBody;

    public bool debug = false;
    private bool initiated = false;

    private float oldSpeed;

    // später private machen
    public RaycastHit2D hitCollidedWith;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
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


    // CanMove
    void Movement(Vector2 move, bool yMovement)
    {
        RaycastHit2D[] lines;
        Vector2 currentPos;
        Vector2 startPos;
        Vector2 posDown2;

        GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");

        currentPos = groundCheck.transform.position;

        startPos = currentPos;
        startPos.x = currentPos.x + distanceToTheRight * distanceMultiplicator;

        posDown2 = currentPos;
        posDown2.y = posDown2.y - distanceToFloor;

        Debug.DrawLine(startPos, posDown2, Color.blue);

        lines = Physics2D.LinecastAll(startPos, posDown2);

        isWalking = false;

        float distance = move.magnitude;
        //Debug.Log("Distance: "+distance);

        if (distance > minMoveDistance)
        {

            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            Debug.Log("hitBufferList.Count: " +hitBufferList.Count);
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                //Debug.Log("HitBuffer: " + hitBufferList[i].collider.name);

                hitCollidedWith = hitBufferList[i];

                if (hitBufferList[i].collider.tag == "obstacle")
                {

                    startPos = transform.position;
                    startPos.x = currentPos.x + distanceToTheRight * distanceMultiplicator;
                    startPos.y += 0.007f;
                    Vector2 wallDistance = startPos + new Vector2(distanceToWall * distanceMultiplicator, 0);

                    Debug.DrawLine(startPos, wallDistance, Color.red);
                    RaycastHit2D[] wallHits = Physics2D.LinecastAll(startPos, wallDistance);

                    foreach (RaycastHit2D hit2 in wallHits)
                    {
                        Debug.Log("Hit2: " + hit2.collider.name);
                        if (!(hit2.collider.tag == "obstacle") && !(hit2.collider.tag == "Treasure"))
                        {

                            isWalking = true;

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

                }

                /*
                if (hitBufferList[i].collider.tag == "Treasure")
                {
                    isWalking = false;
                }*/

            }

            if (isWalking)
            {
                Debug.Log("Yoa bin drin");
                rb2d.position = rb2d.position + move.normalized * distance;
                maxSpeed = oldSpeed;
            }
            else
            {
                Debug.Log("Ja nee");
                //rb2d.position = rb2d.position + move.normalized * distance;
            }

            //rb2d.position = rb2d.position + move.normalized * distance;
           
        }

    }


    protected void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = maxSpeed; //Input.GetAxis("Horizontal"); // fester Wert?

        /*

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }*/
        

        //animator.SetBool("Ground", grounded);
        //animator.SetFloat("Speed", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }


    void Start()
    {
        rBody = this.GetComponent<Rigidbody2D>();
        m_Anim = this.GetComponent<Animator>();
        oldSpeed = maxSpeed;

        if (distanceMultiplicator == 0)
        {
            Debug.LogWarning("distanceMultiplicator: not assigned, using default value.");
            distanceMultiplicator = 1.0f;
        }

        if (maxSpeed == 0)
        {
            Debug.LogWarning("moveGroundSpeed: not assigned, using default value.");
            maxSpeed = 0.04f;
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

        m_Anim.SetBool("Ground", true);

        if (isWalking)
        {
            m_Anim.SetFloat("Speed", 0.02f);
        }
        else
        {
            m_Anim.SetFloat("Speed", 0);
        }

        targetVelocity = Vector2.zero;
        ComputeVelocity();
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

        currentPosition.x += move;
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

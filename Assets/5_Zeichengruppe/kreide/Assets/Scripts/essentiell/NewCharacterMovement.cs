using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class NewCharacterMovement : MonoBehaviour
{
    // Adujustable Raycast Modifier
    public float moveSpeedMultiplicator;
    public float moveGroundSpeed;
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

    // später private machen
    public RaycastHit2D hitCollidedWith;
    private Vector2 desiredVelocity;

    private 

    void Start()
    {
        rBody = this.GetComponent<Rigidbody2D>();
        m_Anim = this.GetComponent<Animator>();

        //Debug.Log("Yo: " + rBody.velocity);
        //Debug.Log(desiredVelocity);
        
        desiredVelocity.Set(desiredVelocity.x, rBody.velocity.y);


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

            /*
            canMoveDiagonally();

            if (isWalking)
            {
                m_Anim.SetFloat("Speed", 0.5f);
                moveDiagonally();
            }
            else
            {
                m_Anim.SetFloat("Speed", 0);
            }*/

     
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

            hitCollidedWith = line;

            if (debug)
            {
                //Debug.Log(""
                //"Down hits: " + line.transform.gameObject.name
                //+ "\n\tTag: " + line.collider.tag
                //+ "\n\tDistance to Line: " + line.distance
                //+ "\tFraction: " + line.fraction
                //+ "\tPoint: " + line.point
                //+ "");
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
                    if (angle.collider.tag == "obstacle")
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
                        }
                        else if (angleFraction <= 0.12)
                        {
                            // down
                        }

                        //isWalking = true;
                        if (angleDistance < line.distance)
                        { // 3 zu 1
                            isMovingUp = true;
                            lineDifference = angleDistance - line.distance;


                        }
                        else if (angleDistance > line.distance)
                        {
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
                    //Debug.Log("isWalking: " + isWalking);
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
            hitCollidedWith = hit2;

            if (debug)
            {
                //Debug.Log("Right hits: " + hit2.collider.name);
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

        //Debug.Log("isWalking: " + isWalking);

}
 

    public void moveDiagonally()
    {
        rBody.velocity = desiredVelocity;
    }
    // Checks if the Player can move by checking for a collider, sets isWalking to either true or false
    public void canMoveDiagonally()
    {
        RaycastHit2D[] lines;
        Vector2 posCurrent, posStart, posDown;
        //GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
        //GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");


        //posCurrent = groundCheck.transform.position; //???
        posCurrent = this.GetComponent<CircleCollider2D>().transform.position; //???

        posStart = posCurrent;
        posStart.x = posCurrent.x + distanceToTheRight * distanceMultiplicator;

        posDown = posCurrent;
        posDown.y = posDown.y - distanceToFloor;

        Debug.DrawLine(posStart, posDown, Color.blue); // Down

        // Checks if the player is grounded
        lines = Physics2D.LinecastAll(posStart, posDown);
        //hits = Physics2D.RaycastAll(posToCheck, posDown2); //posDown

        isWalking = false;
        isMovingUp = false;
        isMovingDown = false;

        // Weird function from Web
        float slopeRayHeight = 10f, steepSlopeAngle = 10f, slopeThreshold = 0.01f;
        Ray myRay = new Ray(posStart, posDown); // cast a Ray from the position of our gameObject into our desired direction. Add the slopeRayHeight to the Y parameter.

        foreach (RaycastHit2D hit in lines)
        {
            if (hit.collider.gameObject.tag == "obstacle") // Our Ray has hit the ground
            {
                float slopeAngle = Mathf.Deg2Rad * Vector3.Angle(Vector3.up, hit.normal); // Here we get the angle between the Up Vector and the normal of the wall we are checking against: 90 for straight up walls, 0 for flat ground.

                float radius = Mathf.Abs(slopeRayHeight / Mathf.Sin(slopeAngle)); // slopeRayHeight is the Y offset from the ground you wish to cast your ray from.

                if (slopeAngle >= steepSlopeAngle * Mathf.Deg2Rad) //You can set "steepSlopeAngle" to any angle you wish.
                {
                    if (hit.distance - this.GetComponent<CircleCollider2D>().radius > Mathf.Abs(Mathf.Cos(slopeAngle) * radius) + slopeThreshold) // Magical Cosine. This is how we find out how near we are to the slope / if we are standing on the slope. as we are casting from the center of the collider we have to remove the collider radius.
                                                                                                                                                       // The slopeThreshold helps kills some bugs. ( e.g. cosine being 0 at 90° walls) 0.01 was a good number for me here
                    {
                        isWalking = true; // return true if we are still far away from the slope
                    }

                    isWalking = false; // return false if we are very near / on the slope && the slope is steep
                }

                isWalking = true; // return true if the slope is not steep

            }
        }
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

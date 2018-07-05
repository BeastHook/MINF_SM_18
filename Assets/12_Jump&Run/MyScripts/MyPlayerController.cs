using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyPlayerController : MonoBehaviour
{

    //component variables
    Animator anim;
    Rigidbody rigid;
    CapsuleCollider playerCollision;
    public DestinationController dest;

    //reference variables
    public Canvas canvas;
    public Image PauseImage;
    public Text Infotext;
    public Text Attemptstext;

    //groundcheck variables
    public Transform groundCheck;
    bool grounded;

    //movement variables
    public float moveSpeed = -3;
    public float jumpforce = 350;

    //safety variables for pausing the game if main marker is lost
    bool gameIsRunning = false;
    bool gameRestarting = false;
    bool levelTracked = false;
    bool endOfGame = false;
   


    //sliding variables
    float colliderHeight = 2;
    float colliderCenter = 0;
    public bool isSliding = false;

    Vector3 StartPosition;
    public GameObject Destination;
    bool destinationReached= false;
    float attempts;
    VariableContainer container;
    


    // Use this for initialization
    void Start()
    {
        container = FindObjectOfType<VariableContainer>();

        

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        playerCollision = GetComponent<CapsuleCollider>();

        PauseGame(true);
        canvas.enabled = true;
        attempts = container.attempts;
        string retryString = "Übrige Versuche: " + attempts;

        



     

        if (attempts == 0)
        {
            Attemptstext.text = "";
            Infotext.text = "Spiel Vorbei";
            Infotext.color = Color.red;
            PauseImage.color = Color.black;
        }

        if (attempts < 3&&attempts!=0)
        {
            Attemptstext.text = retryString;
            
        }



        //get character collider variables
        colliderHeight = playerCollision.height;
        colliderCenter = playerCollision.center.y;

        StartPosition = transform.localPosition;

    }


    void Update()
    {





        //Collider anpassen
        SetCrouchHitbox();

        if (Destination.transform.position.x<0&&moveSpeed<0)
        {
         




            moveSpeed += 0.5f * Time.deltaTime;
            if (moveSpeed > 0)
            {
                if (!destinationReached)
                {
                    destinationReached = true;
                    dest.OpenChest();
                }
                moveSpeed = 0f;
            }
           
            float value = moveSpeed * 100 / -1.5f;
            anim.SetFloat("walkSpeed", value/100);
           
        }


        if (StartPosition.x - 1 > transform.localPosition.x)
        {

            if (!gameRestarting)
            {
                gameRestarting = true;
               container.attempts -= 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        //Bodenkontaktabfrage
        if (Physics.Raycast(groundCheck.position, new Vector3(0, -1, 0), 1f))
        {
            grounded = true;
            anim.SetBool("grounded", true);
        }
        else
        {
            grounded = false;
            anim.SetBool("grounded", false);
        }







        //vorläufiger Unpause Knopf!!!!!
        if (Input.GetButtonDown("Jump"))
        {
            if (levelTracked && !gameIsRunning)
            {
                StartCoroutine(WaitForRealTime(1f));
            }
            if (gameIsRunning)
            {
                TriggerInput(2);
            }

        }



        if (Input.GetButtonDown("Fire1"))
        {
            if (gameIsRunning)
            {
                TriggerInput(3);
            }
        }
    }

    public void TriggerInput(int markerNumber)
    {


        if (gameIsRunning)
        {
            if (markerNumber == 1)
            {
                PauseGame(true);
            }

            if (markerNumber == 2)
            {
                if (!isSliding)
                {
                    rigid.AddForce(new Vector3(0, jumpforce, 0));
                    anim.SetTrigger("Jump");
                }
            }

            if (markerNumber == 3)
            {
                if (grounded)
                {
                    anim.SetTrigger("Slide");
                    StartCoroutine(SlidingEnumerator(1f));
                }
            }
        }
        else
        {
            if (levelTracked && !gameIsRunning)
            {
                if (attempts < 1) { 
                StartCoroutine(WaitForRealTime(3f));
            }
                else
                {
                StartCoroutine(WaitForRealTime(1f));
                }


            }
        }

        if (markerNumber == 1)
        {
            //pause mechanism if marker lost
            StopAllCoroutines();
            PauseImage.fillAmount = 1f;
        }
    }

    public void PauseGame(bool value)
    {
        if (Time.timeScale == 1.0f && value)
        {
            Time.timeScale = 0.0f;
            gameIsRunning = false;
            canvas.enabled = true;
        }
        if (Time.timeScale == 0.0f && !value)
        {
            Time.timeScale = 1.0f;
            gameIsRunning = true;
            canvas.enabled = false;
        }
    }

    public void SetLevelTracked(bool value)
    {
        levelTracked = value;
    }

    public IEnumerator WaitForRealTime(float delay)
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                if (attempts > 0)
                {

                    float value = pauseEndTime - Time.realtimeSinceStartup;
                    if (PauseImage)
                    {
                        PauseImage.fillAmount = value * 100 / delay / 100;
                    }
                }
                yield return 0;
            }


            if (attempts > 0)
            {
                PauseGame(false);
            }
            else
            {
                EndApplication();
            }

            if (PauseImage)
            {
                PauseImage.fillAmount = 1f;
            }
            break;
        }
    }

    void SetCrouchHitbox()
    {
        if (isSliding)
        {
            if (colliderHeight < 1)
            {
                colliderHeight = 1;
            }
            else
            {
                colliderHeight -= 0.2f * Time.deltaTime * 10;
            }


            if (colliderCenter < -0.5f)
            {
                colliderCenter = -0.5f;
            }
            else
            {
                colliderCenter -= 0.1f * Time.deltaTime * 10;
            }

        }

        if (!isSliding)
        {
            if (colliderHeight > 2)
            {
                colliderHeight = 2;
            }
            else
            {
                colliderHeight += 0.2f * Time.deltaTime * 10;
            }


            if (colliderCenter > 0f)
            {
                colliderCenter = -0f;
            }
            else
            {
                colliderCenter += 0.1f * Time.deltaTime * 10;
            }
        }

        playerCollision.height = colliderHeight;
        playerCollision.center = new Vector3(0, colliderCenter, 0);


    }

    public IEnumerator SlidingEnumerator(float duration)
    {
        while (true)
        {
            isSliding = true;
            anim.SetBool("resumeWalking", false);
            float pauseEndTime = Time.realtimeSinceStartup + duration;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {

                yield return 0;
            }
            anim.SetBool("resumeWalking",true);
            isSliding = false;
            break;
        }
    }
     public void EndApplication()
    {
        print("Beende Application");
        endOfGame = true;
    }



}

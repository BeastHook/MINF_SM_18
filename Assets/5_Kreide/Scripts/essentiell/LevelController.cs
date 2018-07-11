using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ellyn
public class LevelController : MonoBehaviour
{

    //Menu
    private GameObject[] menuElements;
    private GameObject[] resetButton;
    private string lastMovementTrigger;
    //GameObject deleteDrawing;

    private GameObject drawnElementsHolder;
    private GameObject player;
    private AudioSource source;
    private bool isPlaying = false;
    // Chest
    private SpriteRenderer chestColliderWithSprite;
    public Sprite openChestSprite;
    public Sprite closedChestSprite;
    public float showTreasureSpriteDuration = 10f;
    private float tempSpriteDuration;

    // MoveSight variables
    private GameObject[] moveFunctionTrigger;
    private GameObject elementsToMove;
    private Vector3 originalPlayerPosition;
    private Vector3 originalLevelPosition;

    private Vector3 lastObstaclePos;

    private string currentMovementTrigger;

    private int moveSightHitCounter = 0;

    //Distanz um die das Level verschoben werden soll
    public float slideToLeftValue = 0.3f;
    public float lastJump = 0.6f;

    public bool debug = false;

    void Start()
    {

        tempSpriteDuration = showTreasureSpriteDuration;
        chestColliderWithSprite = GameObject.FindWithTag("Treasure").GetComponent<SpriteRenderer>();
        drawnElementsHolder = GameObject.FindWithTag("Drawing");
        source = chestColliderWithSprite.GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");
        menuElements = GameObject.FindGameObjectsWithTag("Menu");
        //resetButton = GameObject.FindGameObjectsWithTag("Reset");
        elementsToMove = GameObject.FindGameObjectWithTag("Level");
        moveFunctionTrigger = GameObject.FindGameObjectsWithTag("MoveSight");

        //ursprüngliche Position der Figur und der Levelelemente sichern
        originalPlayerPosition = player.transform.position;
        originalLevelPosition = elementsToMove.transform.position;

        //Startmenu öffnen
        //toggleGUI(true);

    }

    void Update()
    {
        RaycastHit2D temp = player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith;

        if (temp)
        {
            // Treasure hit
            if (temp.collider.tag == "Treasure")
            {
                if (!isPlaying)
                {
                    isPlaying = true;
                    source.Play();
                    Debug.Log("Playing");
                }
                else
                {
                    Debug.Log("Sound playing");
                }

                chestColliderWithSprite.sprite = openChestSprite;
                showTreasureSpriteDuration -= Time.deltaTime;

                if (debug)
                {
                    Debug.Log("Treasure hit. Seconds left: " + showTreasureSpriteDuration);
                }

                if (showTreasureSpriteDuration < 0f)
                {
                    MultisceneManager.Instance.StartCoroutine(MultisceneManager.Instance.FinishLevel(true));
                }
            }

            if (temp.collider.tag == "MoveSight")
            {


                if (debug)
                {
                    Debug.Log("Movement Trigger berührt. Setze zurück.");
                }
                moveSightHitCounter++;

                lastMovementTrigger = currentMovementTrigger;
                currentMovementTrigger = player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith.collider.name;
                //Debug.Log(lastMovedMovementTrigger);
                //position wo player auf dem obstacle steht speichern, damit man dorthin zurück kann
                //lastObstaclePos = player.transform.position;
                if (!(currentMovementTrigger == lastMovementTrigger))
                {
                    if (currentMovementTrigger == "mft_6")
                    {
                        player.transform.position -= new Vector3(lastJump, 0.0f, 0.0f); //figur verschieben
                        elementsToMove.transform.position -= new Vector3(lastJump, 0.0f, 0.0f); //level verschieben
                    }
                    else
                    {
                        //figur verschieben
                        player.transform.position -= new Vector3(slideToLeftValue, 0.0f, 0.0f);
                        //level verschieben
                        elementsToMove.transform.position -= new Vector3(slideToLeftValue, 0.0f, 0.0f);
                    }
                    //hochzählen auf welchem Auslöser man zuletzt stand, damit man dorthin zurück kann bei resetDrawings
                    //diesen MoveSight ausschalten


                    Debug.Log("??? : " + currentMovementTrigger);
                    GameObject.Find(currentMovementTrigger).SetActive(false);
                    Debug.Log("!!!");
                    lastObstaclePos = player.transform.position;
                }
            }

            if (temp.collider.tag == "border")
            {
                if (debug)
                {
                    Debug.Log("Border! Starte Szene neu.");
                }

                //restartGame();
            }
        }
    }

    //onclick von resetButton
    public void ResetButtonPressed()
    {
        //setzt Figur auf letztes Obstacle
        SetPlayerToLastObstacle();
        //delete the drawings
        DeleteDrawings();
    }

    /// <summary>
    /// Deletes all drawn elements and restores the drawn elements holder object
    /// </summary>
    private void DeleteDrawings()
    {
        if (debug)
        {
            Debug.Log("Destroy Drawings!");
        }
        if (drawnElementsHolder != null)
        {
            Destroy(drawnElementsHolder);
            Instantiate(drawnElementsHolder, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }


    private void SetPlayerToLastObstacle()
    {
        //position of movesight --> movesight löschen?
        if (moveSightHitCounter != 0)
        {
            if (debug)
            {
                Debug.Log("Set Figur to last obstacle!");
            }
            player.transform.position = lastObstaclePos;
        }
        else
        {
            player.transform.position = originalPlayerPosition;
        }
    }

    /// <summary>
    /// Pausiert den Bildschirm und zeigt den Menu Canvas.
    /// </summary>
    public void toggleGUI(bool showMenu)
    {
        if (debug)
        {
            Debug.Log("ToggleGUI: " + showMenu);
        }
        if (showMenu)
        {
            Time.timeScale = 0;
            toggleResetButton(false);
            toggleMenu(true);
        }
        else
        {
            toggleMenu(false);
            toggleResetButton(true);
            Time.timeScale = 1;
        }
    }

    //setzt die Levelelemente und die Figur wieder auf ihre Anfangsposition
    public void restartGame()
    {

        //Application.LoadLevel(Application.loadedLevel);
        isPlaying = false;
        showTreasureSpriteDuration = tempSpriteDuration;

        DeleteDrawings(); //level & Figur zurück an Anfangsposition (3 mal zurück)
        player.transform.position = originalPlayerPosition;
        elementsToMove.transform.position = originalLevelPosition;
        chestColliderWithSprite.sprite = closedChestSprite;  //sprite für treasure zurücksetzen


        foreach (GameObject trigger in moveFunctionTrigger) //moveFunctionTrigger wieder aktivieren
        {
            trigger.SetActive(true);
        }

		//toggleGUI(true);

		MultisceneManager.Instance.StartCoroutine(MultisceneManager.Instance.FinishLevel(true));
	}

    //zeigt das menu
    public void toggleMenu(bool value)
    {
        foreach (GameObject m in menuElements)
        {
            m.SetActive(value);
        }
    }

    public void toggleResetButton(bool value)
    {
        foreach (GameObject r in resetButton)
        {
            r.SetActive(value);
        }
    }

}

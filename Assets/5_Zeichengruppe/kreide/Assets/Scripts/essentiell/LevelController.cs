using System.Collections;
using UnityEngine;

// Ellyn
public class LevelController : MonoBehaviour
{

    //Menu
    private GameObject[] menuElements;
    private GameObject[] resetButton;
    //GameObject deleteDrawing;

    private GameObject drawnElementsHolder;
    private GameObject player;
    private AudioSource source;
    private bool isPlaying = false;
    // Chest
    private SpriteRenderer chestColliderWithSprite;
    public Sprite openChestSprite;
    public Sprite closedChestSprite;
    public float showTreasureSpriteDuration = 5.5f;

    // MoveSight variables
    private GameObject[] moveFunctionTrigger;
    private GameObject elementsToMove;
    private Vector3 posPlayer;
    private Vector3 posLevel;

    private Vector3 lastObstaclePos;

    private string lastMovedMovementTrigger;

    private int moveSightHitCounter = 0;

    //Distanz um die das Level verschoben werden soll
    private float slideToLeftValue = 0.3f;
    private float lastJump = 0.6f;

    public bool debug = false;

    void Start()
    {

        chestColliderWithSprite = GameObject.FindWithTag("Treasure").GetComponent<SpriteRenderer>();
        drawnElementsHolder = GameObject.FindWithTag("Drawing");
        source = chestColliderWithSprite.GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");
        menuElements = GameObject.FindGameObjectsWithTag("Menu");
        resetButton = GameObject.FindGameObjectsWithTag("Reset");
        elementsToMove = GameObject.FindGameObjectWithTag("Level");
        moveFunctionTrigger = GameObject.FindGameObjectsWithTag("MoveSight");

        //ursprüngliche Position der Figur und der Levelelemente sichern
        posPlayer = player.transform.position;
        posLevel = elementsToMove.transform.position;

        //Startmenu öffnen
        toggleGUI(true);

    }

    void Update()
    {

        if (player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith)
        {
            // Treasure hit
            if (player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith.collider.tag == "Treasure")
            {
              
                if (!isPlaying)
                {
                    isPlaying = true;
                    source.Play();
                    Debug.Log("Playing");
                } else
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
                    restartGame();
                }
            }

            if (player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith.collider.tag == "MoveSight")
            {
                if (debug)
                {
                    Debug.Log("Movement Trigger berührt. Setze zurück.");
                }

                moveSightHitCounter++;
                lastMovedMovementTrigger = player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith.collider.name;
                //position wo player auf dem obstacle steht speichern, damit man dorthin zurück kann
                lastObstaclePos = player.transform.position;
                if (lastMovedMovementTrigger == "mft_6")
                {
                    //figur verschieben
                    player.transform.position -= new Vector3(lastJump, 0.0f, 0.0f);
                    //level verschieben
                    elementsToMove.transform.position -= new Vector3(lastJump, 0.0f, 0.0f);
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
                GameObject.Find(lastMovedMovementTrigger).SetActive(false);
            }

            if (player.GetComponent<CharacterMovementWithSlopesv1>().hitCollidedWith.collider.tag == "border")
            {
                if (debug)
                {
                    Debug.Log("Border! Starte Szene neu.");
                }

                restartGame();
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
            player.transform.position = posPlayer;
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
        DeleteDrawings();
        //level & Figur zurück an Anfangsposition (3 mal zurück)
        player.transform.position = posPlayer;
        elementsToMove.transform.position = posLevel;
        //sprite für treasure zurücksetzen
        chestColliderWithSprite.sprite = closedChestSprite;
        //moveFunctionTrigger wieder aktivieren
        foreach (GameObject trigger in moveFunctionTrigger)
        {
            trigger.SetActive(true);
        }
        toggleGUI(true);

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

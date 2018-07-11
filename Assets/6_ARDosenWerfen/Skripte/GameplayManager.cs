using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

    private List<GameObject> allCans;

    public GameObject[] levelPrefab;
    public static int currentLevel = 0;
    public static int showCurrentLevel = 1;

    public static GameplayManager Instance { set; get; }

    private bool hasWon;
    private bool hasLost;

    public static bool show;
    public static bool hide;
    public Animator chestAnim;
    public Animator priceAnim;


    public static float waitTime;

    private GameObject[] cans;
    private GameObject ground;
    private GameObject[] price;
    private GameObject[] schilder;
    private GameObject[] ball;
    private GameObject[] blocker;
    private GameObject[] chest;
    private GameObject zeichnung;
    private GameObject[] dots;


    private GameObject arCamera;


    private void Awake() 
    {
        Instance = this;
        hasWon = false;
        hasLost = false;

        chestAnim.GetComponent<Animator>();
        priceAnim.GetComponent<Animator>();

        // Enable Fadenkreuz und Ballspawn in der Main ARCamera der MultiScene
        arCamera = GameObject.Find("ARCamera");
        arCamera.transform.GetChild(0).gameObject.SetActive(true);
        arCamera.transform.GetChild(1).gameObject.SetActive(true);

        // "MainScene" für das HauptProjekt, ansosnten 6_ARDosenwerfen 
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            CreateLevel();
        }

    }
         
        public void ChangeScene(string sceneName) 
        {
            SceneManager.LoadScene(sceneName);
        }

    void FixedUpdate ()
    {
        //GEWONNEN 
        if (hasWon)
        {         
            Victory();
            Shoot.allowedToShoot = false;
            // Fadenkreuz und Ballspawn wird deaktiviert in der Main ARCamera -> Spieler geht weiter zur nächsten Experience
            arCamera.transform.GetChild(0).gameObject.SetActive(false);
            arCamera.transform.GetChild(1).gameObject.SetActive(false);
        }

        //VERLOREN
        if (hasLost)
        {
            Shoot.allowedToShoot = false;
            waitTime += Time.deltaTime;

            // Wartezeit
           if (waitTime >= 4.0)
           {
               //Ursprünglich: erneutes laden der MainScene
               //ChangeScene("MainScene");
	           MultisceneManager.Instance.StartCoroutine(MultisceneManager.Instance.FinishLevel(true));

				currentLevel = 0;
               ScoreManager.score = 0;
               waitTime = 0;
               showCurrentLevel = 1;
           }
        }

        // Level beendet weil alle bälle aufgebraucht sind
        if (Shoot.canShoot == false)
        {
            // WIN METHOD     
             NextLevel();           
        }

        if (show)
        {
            ShowLevel();
        }

        if (hide)
        {
            HideLevel();
        }


    }

    // GAMEPLAYLOGIK
    // ================     
    public void OnLevelWasLoaded (int levelIndex)
    {
     
    }

    private void CreateLevel()
    {
        Shoot.ballAmount = 3;
        Shoot.elapsedTime = 3;
        // Instantiating level prefab
        if (currentLevel < levelPrefab.Length)
        {
            // neue levels geladen
            Instantiate(levelPrefab[currentLevel]);          
        }
        else
        {
            // Siegpunktzahl erreicht
            if(ScoreManager.score >= 250)
            {
                hasWon = true;
                FindObjectOfType<AudioManagerDosenwerfen>().Play("gameSuccesfull");
                Shoot.allowedToShoot = false;
            }
            else
            {
                hasLost = true;
                FindObjectOfType<AudioManagerDosenwerfen>().Play("gameFailed");
                Shoot.allowedToShoot = false;
            }
        }

        GameObject[] a = GameObject.FindGameObjectsWithTag("can");
        allCans = new List<GameObject>();

        allCans.AddRange(a);
    }



    public void RemoveCans(GameObject cans)
    {
        // Validierung der getroffenen Cans
        if (allCans.Find(b=>b == cans))
        {
          
            allCans.Remove(cans);
          
            Renderer meshCans= cans.GetComponent<Renderer>();
            meshCans.material.SetColor("_Color", Color.green);
            ScoreManager.AddPoints(10);
            FindObjectOfType<AudioManagerDosenwerfen>().Play("validate");

        }
    }

    public void NextLevel()
    {
        currentLevel++;
        showCurrentLevel++;
        

        if (currentLevel != 3)
        {
            FindObjectOfType<AudioManagerDosenwerfen>().Play("levelComplete");
        }
        if (currentLevel != 3)
        {
            // Aus alter Szene/level alle cans löschen
            cans = GameObject.FindGameObjectsWithTag("can");
            for (int i = 0; i < cans.Length; i++)
                DestroyImmediate(cans[i]);

            // Aus alter Szene/level alle bälle löschen
            GameObject[] ball = GameObject.FindGameObjectsWithTag("ball");
            for (int i = 0; i < ball.Length; i++)
                DestroyImmediate(ball[i]);

            schilder = GameObject.FindGameObjectsWithTag("Schild");
            for (int i = 0; i < schilder.Length; i++)
                DestroyImmediate(schilder[i]);

            ground = GameObject.FindGameObjectWithTag("ground");
            DestroyImmediate(ground);    
        }
        CreateLevel();
    }




    public void HideLevel ()
    {
        Shoot.allowedToShoot = false;
        cans = GameObject.FindGameObjectsWithTag("can");
        ground = GameObject.FindGameObjectWithTag("ground");
        schilder = GameObject.FindGameObjectsWithTag("Schild");
        ball = GameObject.FindGameObjectsWithTag("ball");
        blocker = GameObject.FindGameObjectsWithTag("blocker");
        chest = GameObject.FindGameObjectsWithTag("chest");
        zeichnung = GameObject.FindGameObjectWithTag("zeichnung");
        dots = GameObject.FindGameObjectsWithTag("dot");
        price =  GameObject.FindGameObjectsWithTag("price");
        

        GameObject ballText = GameObject.Find("GameText");
        ballText.GetComponent<Text>().enabled = false;

        SpriteRenderer zeichnungRend = zeichnung.GetComponent<SpriteRenderer>();
        zeichnungRend.enabled = false;

       

        for (int i = 0; i < price.Length; i++)
        {
            MeshRenderer priceRend = price[i].GetComponent<MeshRenderer>();
            priceRend.enabled = false;
        }

        for (int i = 0; i < dots.Length; i++)
        {
            SpriteRenderer dotsRend = dots[i].GetComponent<SpriteRenderer>();
            dotsRend.enabled = false;
        }

        for (int i = 0; i < chest.Length; i++)
        {
            MeshRenderer chestRend = chest[i].GetComponent<MeshRenderer>();
            chestRend.enabled = false;
        }

        
                for (int i = 0; i < blocker.Length; i++)
                {
                    MeshRenderer blockerRend = blocker[i].GetComponent<MeshRenderer>();
                    blockerRend.enabled = false;
                }
                
        for (int i = 0; i < ball.Length; i++)
        {
            MeshRenderer ballRend = ball[i].GetComponent<MeshRenderer>();
            ballRend.enabled = false;
        }
           

        for (int i = 0; i < cans.Length; i++)
        {
            MeshRenderer rend = cans[i].GetComponent<MeshRenderer>();
            MeshRenderer groundRend = ground.GetComponent<MeshRenderer>();

            groundRend.enabled = false;
            rend.enabled = false;
        }

        for (int i = 0; i < schilder.Length; i++)
        {

            MeshRenderer schildRend = schilder[i].GetComponent<MeshRenderer>();
            schildRend.enabled = false;
        }
    }

    public void ShowLevel ()
    {

        // Am Spiellende wird das schiessen verhindert
        if (currentLevel != 3)
        {
            Shoot.allowedToShoot = true;
        }
     
        cans = GameObject.FindGameObjectsWithTag("can");
        ground = GameObject.FindGameObjectWithTag("ground");
        schilder = GameObject.FindGameObjectsWithTag("Schild");
        ball = GameObject.FindGameObjectsWithTag("ball");
         blocker = GameObject.FindGameObjectsWithTag("blocker");
        chest = GameObject.FindGameObjectsWithTag("chest");
        zeichnung = GameObject.FindGameObjectWithTag("zeichnung");
        dots = GameObject.FindGameObjectsWithTag("dot");
        price = GameObject.FindGameObjectsWithTag("price");

        GameObject ballText = GameObject.Find("GameText");
        ballText.GetComponent<Text>().enabled = true;
   
        SpriteRenderer zeichnungRend = zeichnung.GetComponent<SpriteRenderer>();
        zeichnungRend.enabled = true;

        for (int i = 0; i < price.Length; i++)
        {
            MeshRenderer priceRend = price[i].GetComponent<MeshRenderer>();
            priceRend.enabled = true;
        }

        for (int i = 0; i < dots.Length; i++)
        {
            SpriteRenderer dotsRend = dots[i].GetComponent<SpriteRenderer>();
            dotsRend.enabled = true;
        }

        for (int i = 0; i < chest.Length; i++)
        {
            MeshRenderer chestRend = chest[i].GetComponent<MeshRenderer>();
            chestRend.enabled = true;
        }


        for (int i = 0; i < ball.Length; i++)
        {
            MeshRenderer ballRend = ball[i].GetComponent<MeshRenderer>();
            ballRend.enabled = true;
        }
        
        for (int i = 0; i < blocker.Length; i++)
        {
            MeshRenderer blockerRend = blocker[i].GetComponent<MeshRenderer>();
            blockerRend.enabled = true;
        }
        
        for (int i = 0; i < cans.Length; i++)
        {
            MeshRenderer rend = cans[i].GetComponent<MeshRenderer>();
            MeshRenderer groundRend = ground.GetComponent<MeshRenderer>();

            groundRend.enabled = true;
            rend.enabled = true;

        }

        for (int i = 0; i < schilder.Length; i++)
        {

            MeshRenderer schildRend = schilder[i].GetComponent<MeshRenderer>();
            schildRend.enabled = true;
        }
    }

    public void Victory()
    {
        chestAnim.SetBool("open", true);
        priceAnim.SetBool("getPrice", true);
        showCurrentLevel = 3;

    }


}

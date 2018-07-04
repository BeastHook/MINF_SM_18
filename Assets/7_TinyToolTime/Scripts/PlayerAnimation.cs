using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//All Animations
public class PlayerAnimation : MonoBehaviour
{
   
    public int levelGet;
    public ParticleSystem waterParticle;
    public ParticleSystem chestParticle;
    public GameObject chest;
    public int walkDamage = 0;
    public GameObject secondPlayer;
    public GameObject water;
    public GameObject e;
    public GameObject tree1;
    public GameObject tree2;
    String nameTool;
    public GameObject brokenBucketPrefab;
    private GameObject brokenBucket;
    private PlayerMovementTiny playerMovement;
    private Animator anim;
    private Renderer rend;  
    private GameObject newPosPlayer;
    private GameObject obstacle;
    private SoundManager audioM;
    float flipLocalScale;
    private int damageInteger;

    private void Start()
    {
        damageInteger = 0;
        playerMovement = GetComponent<PlayerMovementTiny>();
        anim = GetComponent<Animator>();
        rend = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        audioM = GetComponent<SoundManager>();
        flipLocalScale = transform.localScale.x;
    }


    void GoAhead()
    {
        Debug.Log("Weiter gehts: ");
        playerMovement.SetCanWalk(true); //Player Walk
    }

    public void PlayAnimation(int animInt) //Called by HelpWindow
    {
        anim.SetInteger("Animation", animInt);      

    }

    internal void Walk(bool v)
    {
        anim.SetBool("walk", v); //Start Walk Animation
        anim.SetInteger("Animation", 10); //Stop EventAnimation
    }

    internal void GameOver() //Finnish the Game
    {
 //       anim.SetBool("walk", false);
        anim.SetInteger("Animation", 12);
    }

    //***************************ANIMATIONS Events************************
    /* Every Animation need  
     *      ShowAnimatedTool  
     *      HideAnimatedTool 
     *      (switch Integer: 1 - axe; 2 - bucket; 3 - tramp;)
     * 
     * Every Right Solution Animation need  
     *      RightToolWalk Event(At the End)    
     *      SwitchPlayerEvent
     *      
     * Damage Animation need a GetDamage Event
     * 
     * Idle Animation has the SwitchPlayerBackEvent
     * 
     * BucketAnimation need the StartWaterParticle 
     * */

    public void SwitchPlayerEvent()
    {
        secondPlayer.transform.SetPositionAndRotation(newPosPlayer.transform.position, newPosPlayer.transform.rotation); //setPosition Player 2
        rend.enabled = false;//deactivate Player 1      
        secondPlayer.SetActive(true); //activate Player 2
        gameObject.transform.SetPositionAndRotation(newPosPlayer.transform.position, newPosPlayer.transform.rotation); //Wieder auf Ausgangsposition       
    }

    void SwitchPlayerBack()
    {
        secondPlayer.SetActive(false); //Deaktiviert den zweiten Spieler
        rend.enabled = true;
        RightToolWalk();
    }

    public void SkipPlayer()
    {      
        flipLocalScale *= -1;       
        gameObject.transform.localScale = new Vector3(flipLocalScale, transform.localScale.y, transform.localScale.z);
       
    }

    //BUCKET ###########################
    public void StartWaterParticle() {
        waterParticle.Play();
        print("WaaaterPart#######################");
        audioM.ChooseSound(2);
    }

    public void ReplaceBrokenBucket(AnimationEvent aniEvent)
    {       
        HideAnimatedTool(aniEvent);
        brokenBucket = Instantiate(brokenBucketPrefab, brokenBucketPrefab.transform.position, brokenBucketPrefab.transform.rotation);
        audioM.ChooseSound(9);
    }

    public void ShowWater()
    {
        water.SetActive(true);
    }

    //Chest ########
    public void HideChest()
    {
        chest.SetActive(false);
    }
    public void StartChestParticle()
    {
        chestParticle.Play();
        e.SetActive(true);
    }

    //########## Tree #############
    public void StartTreeAnimation(AnimationEvent aniEvent) {

        if(aniEvent.intParameter == 1)
             tree1.GetComponent<TreeAnimation>().StartAnimation(); //
        else
            tree2.GetComponent<TreeAnimation>().StartAnimation();
    }


    //########### Show & Hide TOOLS ################# (Child) 1 - axe; 2 - bucket; 3 - tramp; 
    public void ShowAnimatedTool(AnimationEvent aniEvent)
    {
        gameObject.transform.GetChild(aniEvent.intParameter).gameObject.SetActive(true);
    }

    public void HideAnimatedTool(AnimationEvent aniEvent)
    {
        gameObject.transform.GetChild(aniEvent.intParameter).gameObject.SetActive(false);
        ResetAnimationInteger(); //Reset the AnimationTrigger       
    }

    public void ResetAnimationInteger()
    {
        anim.SetInteger("Animation", 10); //Reset the AnimationInteger
    }

    //###### Walk #########
    void RightToolWalk() //Event in Right Solution
    {
        Walk(true);
        GoAhead();
    }

    //ShowHelp ###########
    public void SetObstacle(GameObject obstacle) { //Called by PlayerMovement
        this.obstacle = obstacle;
        ShowHelp();
        SetPositionSwitchPlayer();      
    }

    private void SetPositionSwitchPlayer() //Set the new Position for the SecondDummy for Switch
    {
        newPosPlayer = obstacle.GetComponent<Obstacle>().PositionSecondPlayer;
    }

    public void ShowHelp() //Lässt das HelferFragezeichen aufploppen
    {
        if (!obstacle.GetComponent<Obstacle>().HelpWindow.activeSelf)
        {
            obstacle.GetComponent<Obstacle>().ShowHelpWindow(true, gameObject);
            audioM.ChooseSound(7);
        }
    }

    //############# DAMAGE #############

    public void GetDamage(AnimationEvent aniEvent) { //Get Damage, change Walking animations
        if (!aniEvent.intParameter.Equals(damageInteger))
        {
            walkDamage += 1;
            anim.SetInteger("WalkState", walkDamage);
            playerMovement.ReduceSpeed();
            print("Actual Speed");
            damageInteger = aniEvent.intParameter;
        }
    }

    //############## Easter egg ##################
    public void ActivateEasterEgg() {
        print("EasterEgg");
        anim.SetBool("EasterEgg",true);
    }



}
    

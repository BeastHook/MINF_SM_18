using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindow : MonoBehaviour {

    private int actualTool;
    private GameObject player;
    private PlayerAnimation playerAnim;
    private int level;
    public ParticleSystem burst;



    public void SetPlayer(GameObject player, int level)//Called by Obstacle
    {
        this.player = player;
        this.level = level;
        playerAnim = player.GetComponent<PlayerAnimation>();
    }

    public void ContactWithTool(int toolInt) //Called by tool when tool touched the HelpWindow
    {
       
        actualTool = toolInt;
        print("GetTool Name: " + actualTool);
        playerAnim.PlayAnimation(actualTool+level*3); //0 = erstes tool, 1 = zweites toll, 3 = drittes tool / 0 = erstes level usw...
        burst.Play(true);
        gameObject.SetActive(false);
    }


}

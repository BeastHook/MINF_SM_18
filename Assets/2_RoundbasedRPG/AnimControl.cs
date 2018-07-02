using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour {

    public Animator anim;
    int attack01;
    int attack02;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        attack01 = Animator.StringToHash("attack01");
        attack02 = Animator.StringToHash("attack02");

    }

    public void Attack01()
    {
        anim.SetTrigger(attack01);
    }

    public void Attack02()
    {
        anim.SetTrigger(attack02);
    }
}

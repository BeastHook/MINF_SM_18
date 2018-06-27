﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Animator animator;
    private Text scoreText;

    void Start ()
    {

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        scoreText = animator.GetComponent<Text>();
    }

    public void SetText (string text)
    {
        animator.GetComponent<Text>().text = text;
    }


}

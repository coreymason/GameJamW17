﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

	// Use this for initialization
	void Awake ()
    {
        animator = gameObject.GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckJump();
        CheckMovement();
    }

    void CheckJump()
    {
        animator.SetBool("Grounded", Player.current.isGrounded);
        animator.SetFloat("Jump", Player.current.rb.velocity.y);
    }

    void CheckMovement()
    {
        animator.SetFloat("Walk Speed", Mathf.Abs(Player.current.rb.velocity.x));
    }
}

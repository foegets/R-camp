using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();  
    }
    void Update()
    {
        AnimationCheck();
    }
  

    public void AnimationCheck()
    {
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("IsGround", physicsCheck.isGround);
        anim.SetFloat("velocityX", Math.Abs(rb.velocity.x));
        anim.SetBool("IsAttack", playerController.isAttack);
        
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
    }
}

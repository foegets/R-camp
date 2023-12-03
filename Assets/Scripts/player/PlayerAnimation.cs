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
    private Character character;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();  
        playerController = GetComponent<PlayerController>();    
        character = GetComponent<Character>();  
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
        anim.SetBool("IsDead", playerController.isDead);
        anim.SetBool("IsAttack", playerController.isAttack);
        anim.SetBool("IsDefend", character.isDefensing);
        
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rbo;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rbo = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>(); 
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rbo.velocity.x));
        anim.SetFloat("velocityY", rbo.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isAttack", playerController.isAttack);
        
    }
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }
}

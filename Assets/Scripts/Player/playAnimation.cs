using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private physicsCheck physicsCheck; 


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<physicsCheck>();
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation() 
    {
        anim.SetFloat("horizontalSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY",rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isWalking", physicsCheck.isWalking);
        anim.SetBool("isWallSliding", physicsCheck.isWallSliding);
    }
}

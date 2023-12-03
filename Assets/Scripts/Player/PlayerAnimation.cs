using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private PhysicsCheck physicsCheck;

    private PlayerController playerController;


    private void Awake(){
        anim=GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        physicsCheck=GetComponent<PhysicsCheck>();
        playerController=GetComponent<PlayerController>();
    }

    private void Update(){
        SetAnimation();
    }

    public void SetAnimation(){
        //把PlayerController中的变量和animator中作判断条件的数值关联起来（？）
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY",rb.velocity.y);
        anim.SetBool("isGround",physicsCheck.isGround);
        anim.SetBool("isDead",playerController.isDead);
    }

    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }

}

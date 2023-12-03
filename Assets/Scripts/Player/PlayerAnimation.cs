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
        anim=GetComponent<Animator>();//GetComponent是获取当前游戏对象组件的方法，可以通过直接调用它来访问游戏对象的组件和进行参数调整
        rb = GetComponent<Rigidbody2D>();
        physicsCheck=GetComponent<PhysicsCheck>();
        playerController=GetComponent<PlayerController>();
    }

    private void Update(){
        SetAnimation();
    }

    public void SetAnimation(){
        //把PlayerController中的变量和animator中作判断条件的数值关联起来,更准确的说是赋值！
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

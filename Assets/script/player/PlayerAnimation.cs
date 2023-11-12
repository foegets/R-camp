using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;//创建变量用于获得通过速度切换动画的animator
    private Rigidbody2D rb;//创建变量用于获得速度来传进velocityX
    private PlayerController playerController;
    private PhysicsCheck physicsCheck;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        //传进速度后取绝对值避免向左走速度为负而无法转换动作的问题
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isDead", playerController.isDead);
        anim.SetBool("isGround", physicsCheck.isGround);
    }

    //实现受伤后播放闪烁动画
    public void PlayHurt()
    {
        anim.SetTrigger("hurt trigger");       
    }
}

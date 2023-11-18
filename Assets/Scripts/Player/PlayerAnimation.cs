using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //创建anim变量来访问animator
    private Animator anim;
    //获得刚体的组件
    private Rigidbody2D rb;
    //访问物理检测组件
    private PhysicsCheck physicsCheck;
    //访问人物控制代码
    private PlayerController playercontroller; 

    private void Awake(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playercontroller = GetComponent<PlayerController>();
    }
    
    private void Update(){
        SetAnimation();
    }

    public void SetAnimation(){
        //把x轴上的速度给到animator
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        //把y轴上的速度给到animator
        anim.SetFloat("velocityY",rb.velocity.y);
        //把物理检测中是否在地上的结果给到animator
        anim.SetBool("isGround",physicsCheck.isGround);
        //把人物控制代码中是否死亡的结果给到animator
        anim.SetBool("isDead",playercontroller.isDead);
        //把人物控制代码中的攻击状态添加进来
        anim.SetBool("isAttack",playercontroller.isAttack);
    }

    //创建（受伤动画的触发器）函数方法
    public void PlayHurt(){
        anim.SetTrigger("hurt");
    }

    //创建（玩家攻击的触发器）的函数方法
    public void PlayerAttack(){
        anim.SetTrigger("attack");
    }
}

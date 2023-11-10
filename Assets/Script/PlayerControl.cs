using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //设置速度变量
    public float runSpeed;
    public float jumpSpeed;

    //设置动画
    //记得设置！！！！！！！！！
    public SkeletonAnimation chooseAnimation;
    public AnimationReferenceAsset idle, attack;
    private string currentState;
    bool attackFinish;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        //获取Player物理模型
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();

        //初始化动画
        currentState = "Idle";
        SetCharacterState(currentState);
        attackFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnGround();
        Move();
        Jump();
        Flip();
        Attack();
    }

    void Move()
    {
        //控制玩家移动
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        //通过改变速度进行移动
        myRigidbody.velocity = playerVel;
        if(moveDir != 0)
        {
            SetCharacterState("Idle");
        }
        else
        {
            SetCharacterState("Idle");
        }

    }

    void Attack()
    {
        //执行攻击("J"键)
        if (Input.GetButtonDown("Attack"))
        {
            //判断是否正在进行攻击
            if(chooseAnimation.AnimationName == "Attack_Loop")
            {
                return;
            }
            SetCharacterState("Attack");
        }       
    }

    void Flip()
    {
        //判断player速度方向并且改变面朝方向
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);

            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);

            }
        }
    }

    void Jump()
    {
        if(isOnGround)
        {
            //执行跳跃
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
        }
        
    }


    void OnGround()
    {
        //判断是否处于地面上
        isOnGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void AnimationSet(AnimationReferenceAsset animation,bool loop,float timeScale)
    {
        //判断动画状态并执行/切换正确动画
        if (chooseAnimation.AnimationName == "Attack_Loop") 
        {
            if(attackFinish == false)
            {
                //攻击结束切换为原动画
                chooseAnimation.state.AddAnimation(0, animation, loop, 0).TimeScale = timeScale;
                attackFinish = true;
            }   
            return;
        }
        //避免每帧重复开始播放动画
        else if(animation.name.Equals(chooseAnimation.AnimationName))
        {
            return;
        }
        chooseAnimation.state.SetAnimation(0,animation, loop).TimeScale = timeScale;
        attackFinish = false;
    }

    void SetCharacterState(string state)
    {
        if(state.Equals("Idle"))
        {
            AnimationSet(idle, true, 1f);
        }
        else if (state.Equals("Attack"))
        {
            AnimationSet(attack, false, 1f);
        }
    }
}

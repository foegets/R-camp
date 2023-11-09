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
    public SkeletonAnimation texasAnimation;
    public AnimationReferenceAsset idle, attack;
    private string currentState;
    private string currentAnimation;
    bool isAttack;

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
        isAttack = false;
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
        //执行攻击
        if (Input.GetKeyDown(KeyCode.F))
        {
            //判断是否已经
            if(this.GetComponent<SkeletonAnimation>().AnimationName == "Attack_Loop")
            {
                return;
            }
            currentAnimation = null;
            //清空currentAnimation避免动画死循环
            Debug.Log(Input.GetKeyDown(KeyCode.F));
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
        //判断动画状态并执行正确动画
        if (currentAnimation == "Attack_Loop") 
        {
            if(isAttack == false)
            {
                texasAnimation.state.AddAnimation(0, animation, loop, 0).TimeScale = timeScale;
                isAttack = true;
                Debug.Log(isAttack);
            }   
            return;
        }
        else if(animation.name.Equals(currentAnimation))
        {
            return;
        }
        texasAnimation.state.SetAnimation(0,animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
        isAttack = false;
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

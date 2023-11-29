using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;//移动速度
    public float jumpSpeed;//跳跃速度
    public bool canDoubleJump;//用于实现二段跳
    private PhysicsCheck physicsCheck;
    private Rigidbody2D playerRb;
    public float hurtBackDistance;//受伤反弹距离
    public bool isDead;
    private PlayerAnimator playerAnim;
    public bool isHurt;
    
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        physicsCheck=GetComponent<PhysicsCheck>();
        playerAnim = GetComponent<PlayerAnimator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        Jump();
        Attack();
    }

    private void FixedUpdate()
    {
        
    }

    public void Run()
    {
        if(!isDead&&!isHurt)
        {
            float speedMulti = Input.GetAxis("Horizontal");//通过键盘输入的x轴数值
            int faceDirection = (int)transform.localScale.x;
            playerRb.velocity = new Vector2(speedMulti * runSpeed, playerRb.velocity.y);//将x轴速度改变，y轴不变
            if (playerRb.velocity.x > 0)//通过判断x轴速度看是否转向
            {
                faceDirection = 1;
            }
            else if (playerRb.velocity.x < 0)
            {
                faceDirection = -1;
            }
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }
    private void Jump()
    {
        if (!isDead&&!isHurt)
        {
            if (physicsCheck.isGround)//落地重置跳跃
            {
                canDoubleJump = true;
            }
            if (Input.GetButtonDown("Jump"))//按下空格跳跃
            {
                if (physicsCheck.isGround)//在地面直接跳跃
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
                }
                if (!physicsCheck.isGround && canDoubleJump == true)//在空中则跳跃后把canDoubleJump改为false禁止再次跳跃
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
                    canDoubleJump = false;
                }
            }
        }
    }
    public void GetHurt(Transform attacker)//受到伤害直接修改玩家位置达成弹开效果
    {
        transform.position = new Vector2(attacker.position.x - (int)transform.localScale.x * hurtBackDistance, transform.position.y);
    }
    public void IsDead()//将角色改为死亡状态禁用操作
    {
        isDead = true;
        playerRb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            playerAnim.PlayerAttack();
        }
    }
    public void IsHurt()
    {
        isHurt = true;
        playerRb.velocity = Vector2.zero;
    }
    public void IsNotHurt()
    {
        isHurt = false;
    }
    public void  GetMedicine()
    {
        runSpeed += 3;
    }
}

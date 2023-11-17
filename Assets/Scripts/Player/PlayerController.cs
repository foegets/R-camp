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
    
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        physicsCheck=GetComponent<PhysicsCheck>();
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
    }

    private void FixedUpdate()
    {
        
    }

    public void Run()
    {
        float speedMulti = Input.GetAxis("Horizontal");//通过键盘输入的x轴数值
        int faceDirection = (int)transform.localScale.x;
        playerRb.velocity = new Vector2(speedMulti*runSpeed,playerRb.velocity.y);//将x轴速度改变，y轴不变
        if (playerRb.velocity.x > 0)//通过判断x轴速度看是否转向
        {
            faceDirection = 1;
        }
        else if(playerRb.velocity.x < 0)
        {
            faceDirection = -1;
        }
        transform.localScale = new Vector3(faceDirection, 1, 1);
    }
    private void Jump()
    {
        if (physicsCheck.isGround)//落地重置跳跃
        {
            canDoubleJump=true;
        }
        if (Input.GetButtonDown("Jump"))//按下空格跳跃
        {
            if(physicsCheck.isGround)//在地面直接跳跃
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
            }
            if(!physicsCheck.isGround&&canDoubleJump == true)//在空中则跳跃后把canDoubleJump改为false禁止再次跳跃
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
                canDoubleJump=false;
            }
        }
    }
}

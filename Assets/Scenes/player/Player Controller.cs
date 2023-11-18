using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public PlayerInputControl inputControl;//小驼峰
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    
    public float speed; //浮点可以有小数
    public float jumpForce;

    private void Awake()//private没有办法在unity中看见变量，public可以，
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Jump.started += Jump;//注册函数。把函数方法添加按下按键那一刻执行
    }
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    //private void ontriggerstay2d(collider2d collision)
    //{
        
    //}
    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);//如果是使用 transform 的 Position 做加减去移动 ，需要 乘以 Time.deltaTime
        int faceDir =(int) transform.localScale.x;                                         //强制把float 转化为int                                                                 
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir,1,1);//人物翻转
    }
      private void Jump(InputAction.CallbackContext context)
    {
        //
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
          Debug.Log("jump");
       }
        }

    }

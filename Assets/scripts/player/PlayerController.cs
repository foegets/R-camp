using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public PlayerInPutController inputControl;

    private Rigidbody2D rb;//rb用于获取Rigidbody组件的变量


    public SpriteRenderer sr;//sr用于获取SpriteRenderer组件的变量

    [Header("基本参数")]
    public Vector2 inputDirection;//方向

    public float walkSpeed,runSpeek;//移动速度

    public float jumpForce;//跳跃的力

    public float rushForce;

    private PhysicsCheck physicsCheck;//用于获取脚本PhysicsCheck中的变量（isGround）

    private AudioManager audioManager;

    public int jumpNum=0;//记录跳跃次数

    public float leftPressTime, rightPressTime;

    public float maxAwaitTime;

    public bool isWalk,canRun;

    
    

    private void Awake()
    {
        inputControl = new PlayerInPutController();

        rb = GetComponent<Rigidbody2D>();//获取
        sr = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        

        inputControl.gamePlayer.Jump.started += Jump;//事件注册： += （started：按键按下那一刻），Jump方法将在started事件中执行
        inputControl.gamePlayer.Rush.started += Rush;
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
        inputDirection = inputControl.gamePlayer.Move.ReadValue<Vector2>();//获取键盘输入的方向
        //InputTest();
    }

    private void FixedUpdate()
    {
        checkRunWalk();
        Move();//使用移动函数      
        if (physicsCheck.isGround == true)
            jumpNum = 0;
        
    }

    public void Move()//移动函数
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            if (isWalk == true && inputDirection.x != 0)
                rb.velocity = new Vector2(inputDirection.x * walkSpeed * Time.deltaTime, rb.velocity.y);
            if (isWalk == true && canRun == true && inputDirection.x != 0)
                rb.velocity = new Vector2(inputDirection.x * runSpeek * Time.deltaTime, rb.velocity.y);
            if (inputDirection.x == 0)
                rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //修改rigidboy组件中的速度值，实现移动
        //rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //isWalk = true;

        #region 1人物翻转,以修改transform.localScale实现
        //int faceDir= (int)transform.localScale.x;//存储方向
        //if(inputDirection.x>0)
        //    faceDir = 5;//图片素材太小
        //if(inputDirection.x<0)
        //    faceDir = -5;
        //transform.localScale = new Vector3(faceDir, 5, 5);
        #endregion

        #region 人物翻转以修改Sprite Renderer.FlipX实现
        //2人物翻转，以修改Sprite Renderer.FlipX实现
        float faceDir = inputDirection.x;
        
        if (faceDir > 0)
            sr.flipX = false;
        if (faceDir < 0)
            sr.flipX = true;
        #endregion
    }

    private void checkRunWalk()
    {
        if (inputDirection.x > 0 && !isWalk)
        {
            isWalk = true;
            if (Time.time - rightPressTime <= maxAwaitTime)
                canRun = true;
            rightPressTime = Time.time;
        }

        if (inputDirection.x < 0 && !isWalk)
        {
            isWalk = true;
            if (Time.time - leftPressTime <= maxAwaitTime)
                canRun = true;
            leftPressTime = Time.time;
        }
        if(inputDirection.x == 0)
        {
            isWalk = false;
            canRun = false;
        }
        
    }

    private void Rush(InputAction.CallbackContext context)
    {
        Vector2 dir = new Vector2(0,0);
        if(sr.flipX==true)
            dir = new Vector2(-1, 0);
        if(sr.flipX==false)
            dir = new Vector2(1, 0);

        if(physicsCheck.isrushReady==true&&physicsCheck.rushNum>0)
        {
            if(inputDirection.x!= 0||inputDirection.y!=0)
                rb.AddForce(inputDirection*rushForce,ForceMode2D.Impulse);
            if(inputDirection.x==0&&inputDirection.y==0)
                rb.AddForce( dir* rushForce, ForceMode2D.Impulse);

            physicsCheck.rushNum--;
        }
    }

    private void Jump(InputAction.CallbackContext context)//跳跃函数
    {
        //Debug.Log("JUMP!");        
            if (physicsCheck.isGround == true || jumpNum <1)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumpNum++;
                audioManager.PlayFXsoure();
            }
    }  
}

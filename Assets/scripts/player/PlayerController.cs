using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public PlayerInPutController inputControl;

    private Rigidbody2D rb;//rb用于获取Rigidbody组件的变量

    public Camera cam;

    public SpriteRenderer sr;//sr用于获取SpriteRenderer组件的变量

    public UnityEvent<GameObject> E_Fire;

    [Header("基本参数")]
    public Vector2 inputDirection;//方向

    public float walkSpeed,runSpeek;//移动速度

    public float jumpForce;//跳跃的力

    public float rushForce;

    public float hurtForce;

    public int jumpNum=0;//记录跳跃次数
    [Header("计时器")]
    public float leftPressTime, rightPressTime;

    public float maxAwaitTime;
    [Header("状态")]
    public bool isWalk,canRun;

    public Vector3 fireDir;
    public float correct;

    private PhysicsCheck physicsCheck;//用于获取脚本PhysicsCheck中的变量（isGround）
    private AudioManager audioManager;
    private Character character;
    private void Awake()
    {
        inputControl = new PlayerInPutController();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();//获取
        sr = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        character = gameObject.GetComponent<Character>();

        inputControl.gamePlayer.Jump.started += Jump;//事件注册： += （started：按键按下那一刻），Jump方法将在started事件中执行
        inputControl.gamePlayer.Rush.started += Rush;
        inputControl.gamePlayer.Fire.started += Fire;
        
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
        if (physicsCheck.isRush == false&&character.isHurt == false)
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

    private void Rush(InputAction.CallbackContext context)
    {
        Vector2 dir = new Vector2(0,0);
        if(sr.flipX==true)
            dir = new Vector2(-1, 0);
        if(sr.flipX==false)
            dir = new Vector2(1, 0);

        if(physicsCheck.isrushReady==true&&physicsCheck.rushNum>0)
        {
            if (inputDirection.x != 0 || inputDirection.y != 0)
            {
                Debug.Log("rush case 1");
                rb.AddForce(inputDirection * rushForce, ForceMode2D.Impulse);
                physicsCheck.isRush = true;
            }
            if (inputDirection.x == 0 && inputDirection.y == 0)
            {
                Debug.Log("rush case 2");
                rb.AddForce(dir * rushForce, ForceMode2D.Impulse);
                physicsCheck.isRush = true;
            }

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

    private void Fire(InputAction.CallbackContext context)
    {
        E_Fire?.Invoke(gameObject);
    }

    public void GetFireDir()
    {
        Debug.Log("Mouse Left");

        Vector3 playerpos = new Vector3(transform.position.x,transform.position.y + correct, transform.position.z);
        Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);

        fireDir = (mousepos - playerpos);
        fireDir.Normalize();

        Debug.Log(fireDir + "fireDir");
        Debug.Log(playerpos + "playerpos");
        Debug.Log(mousepos + "mousepos");                
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

    public void BeHurt(Transform attacker)
    {
        character.isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x-attacker.transform.position.x), 0.5f).normalized;
        Debug.Log(dir);
        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
         Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    Vector3 playerpos = new Vector3(transform.position.x, transform.position.y + correct, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(mousepos,playerpos);
    }
}

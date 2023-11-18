using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public PlayerInPutController inputControl;

    private Rigidbody2D rb;//rb���ڻ�ȡRigidbody����ı���


    public SpriteRenderer sr;//sr���ڻ�ȡSpriteRenderer����ı���

    [Header("��������")]
    public Vector2 inputDirection;//����

    public float walkSpeed,runSpeek;//�ƶ��ٶ�

    public float jumpForce;//��Ծ����

    public float rushForce;

    private PhysicsCheck physicsCheck;//���ڻ�ȡ�ű�PhysicsCheck�еı�����isGround��

    private AudioManager audioManager;

    public int jumpNum=0;//��¼��Ծ����

    public float leftPressTime, rightPressTime;

    public float maxAwaitTime;

    public bool isWalk,canRun;

    
    

    private void Awake()
    {
        inputControl = new PlayerInPutController();

        rb = GetComponent<Rigidbody2D>();//��ȡ
        sr = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        

        inputControl.gamePlayer.Jump.started += Jump;//�¼�ע�᣺ += ��started������������һ�̣���Jump��������started�¼���ִ��
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
        inputDirection = inputControl.gamePlayer.Move.ReadValue<Vector2>();//��ȡ��������ķ���
        //InputTest();
    }

    private void FixedUpdate()
    {
        checkRunWalk();
        Move();//ʹ���ƶ�����      
        if (physicsCheck.isGround == true)
            jumpNum = 0;
        
    }

    public void Move()//�ƶ�����
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
        //�޸�rigidboy����е��ٶ�ֵ��ʵ���ƶ�
        //rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //isWalk = true;

        #region 1���﷭ת,���޸�transform.localScaleʵ��
        //int faceDir= (int)transform.localScale.x;//�洢����
        //if(inputDirection.x>0)
        //    faceDir = 5;//ͼƬ�ز�̫С
        //if(inputDirection.x<0)
        //    faceDir = -5;
        //transform.localScale = new Vector3(faceDir, 5, 5);
        #endregion

        #region ���﷭ת���޸�Sprite Renderer.FlipXʵ��
        //2���﷭ת�����޸�Sprite Renderer.FlipXʵ��
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

    private void Jump(InputAction.CallbackContext context)//��Ծ����
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

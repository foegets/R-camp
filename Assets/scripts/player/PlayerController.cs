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

    private Rigidbody2D rb;//rb���ڻ�ȡRigidbody����ı���

    public Camera cam;

    public SpriteRenderer sr;//sr���ڻ�ȡSpriteRenderer����ı���

    public UnityEvent<GameObject> E_Fire;

    [Header("��������")]
    public Vector2 inputDirection;//����

    public float walkSpeed,runSpeek;//�ƶ��ٶ�

    public float jumpForce;//��Ծ����

    public float rushForce;

    public float hurtForce;

    public int jumpNum=0;//��¼��Ծ����
    [Header("��ʱ��")]
    public float leftPressTime, rightPressTime;

    public float maxAwaitTime;
    [Header("״̬")]
    public bool isWalk,canRun;

    public Vector3 fireDir;
    public float correct;

    private PhysicsCheck physicsCheck;//���ڻ�ȡ�ű�PhysicsCheck�еı�����isGround��
    private AudioManager audioManager;
    private Character character;
    private void Awake()
    {
        inputControl = new PlayerInPutController();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();//��ȡ
        sr = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        character = gameObject.GetComponent<Character>();

        inputControl.gamePlayer.Jump.started += Jump;//�¼�ע�᣺ += ��started������������һ�̣���Jump��������started�¼���ִ��
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
        if (physicsCheck.isRush == false&&character.isHurt == false)
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

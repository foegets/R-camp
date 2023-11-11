using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameInput inputControl;
    public Vector2 inputDirection;
    public Rigidbody2D rb;
    //������������Awake��ͨ��GetComponent����ж���Ծ�������ײ�����
    private PhysicsCheck physicsCheck;
    [Header("����")]
    public float speed;
    public float jumpForce;
    public float hurtForce;//��������ʩ�����˺󷴵�����
    public bool isHurt;//�ж�����
    public bool isDead;//�ж�����
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new GameInput();
        //ͨ��ʩ������ʵ����Ծ��ע��started��performed��canceled����
        inputControl.Player.Jump.started += Jump;
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
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        //�ж��Ƿ�������ִ��move���������isHurtΪture�Ҳ��䣬�����˺��һֱ�����������Animator�е�blue hurt�����һ�����룬��������������isHurt��Ϊfalse
        if(!isHurt)
            Move();
    }

    public void Move()
    {
        //ʵ���ƶ�
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //ͨ��scaleʵ�����﷭ת
        int faceDir = (int)transform.localScale.x;//��¼��ʵ����
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);//ͨ���ж�ʵ�ַ�ת
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        //������Ծ�ж����������Ծ
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //ʵ�����˷��������빥���ߵĲ���
    public void GetHurt(Transform attacker)
    {
        isHurt = true;//����Fixupdate��ִֹͣ��move
        rb.velocity = Vector2.zero;
        //ͨ���������������жϻ��˷���,normalize����x�����������������������ֵ��һ
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        //�������������������������ͼ�Ϊ2D��˲ʱ
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

    }

    public void PlayerDead()
    {
        isDead = true;
        //����ֱ��������ʵ���޷������ƶ�,���������
        inputControl.Player.Disable();
    }


}   

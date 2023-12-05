using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;//���ô����InputSystem�������������Ǵ�����GameInput�ű�

public class PlayerController : MonoBehaviour
{
    [Header("�¼�����")]
    public SceneLoadEventSO loadEvent;//menu�����������ƶ�
    public VoidEventSO afterSceneLoadedEvent;//menu�л������������ƶ�
    public GameInput inputControl;
    public Vector2 inputDirection;
    public Rigidbody2D rb;
    public PlayerAnimation playerAnimation;
    public Collider2D coll;
    //������������Awake��ͨ��GetComponent����ж���Ծ�������ײ�����
    private PhysicsCheck physicsCheck;
    [Header("����")]
    public float speed;
    public float jumpForce;
    public float hurtForce;//��������ʩ�����˺󷴵�����
    public int numberOfJump;//����������Ծ����
    private int currentJumpNumber;
    [Header("״̬")]
    public bool isHurt;//�ж�����
    public bool isDead;//�ж�����
    public bool isAttack;//�жϹ���
    [Header("����")]
    public PhysicsMaterial2D normal;//��Ħ������ͨ����
    public PhysicsMaterial2D wall;//�⻬������ǽ����ײ�Ĳ���
    
    
    //ÿһ����Ҫ���ýű���������Ҫnewһ��
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new GameInput();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<Collider2D>();
        //��ȡ����������Ծ����ʵ����Ծ��ע��started��performed��canceled����
        inputControl.Player.Jump.started += Jump;
        //��ȡ�������빥����
        inputControl.Player.Attack.started += PlayerAttack;

    }


    //����PlayerController��ʱ��Ҳ����inputControl
    private void OnEnable()
    {
        inputControl.Enable();
        loadEvent.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        inputControl.Disable();
        loadEvent.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }

    private void Update()
    {
        //ʱ�̸������ﳯ��
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
        CheckMaterial();
        ResetNumberOfJump();//������Ծ����
    }
    private void FixedUpdate()
    {
        //�ж��Ƿ����˺͹�����ִ��move����Animator�е�blue hurt�����һ�����룬��������������isHurt��Ϊfalse��isAttack��Ϊfalse
        if(!isHurt && !isAttack)
            Move();       
    }

    private void ResetNumberOfJump()
    {
        if (physicsCheck.isGround)
            currentJumpNumber = numberOfJump;
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Player.Disable();
    }
    private void OnAfterSceneLoadedEvent()
    {
        inputControl.Player.Enable();
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

    //ʵ����Ծ
    private void Jump(InputAction.CallbackContext obj)
    {
        //������Ծ�ж����������Ծ
        if (currentJumpNumber > 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);//����y���ٶȱ�����Ծ������
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            currentJumpNumber--;
        }

    }

    //ʵ�ֹ���
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        //����PlayerAnimation�е�PlayerAttack����
        playerAnimation.PlayerAttack();
        isAttack = true;

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

    
    public void CheckMaterial()
    {
        //ͨ�������Ƿ�ս����������ʵ���ڵ�����Ħ����ʹ���﹥��ʱ���Ử��������ǽʱ��Ħ������Ԫ������������ǵðѺ����Ž�update
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

    public void PlayerDead()
    {
        isDead = true;
        //����ֱ��������ʵ���޷������ƶ�
        inputControl.Player.Disable();
    }


}   

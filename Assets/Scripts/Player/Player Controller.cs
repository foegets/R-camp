using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Vector2 inputDirection;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playAnimation;
    [Header("��������")]
    public float speed;//�����ƶ�
    public float jumpForce;//��Ծ
    public float injureForce;
    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;


    [Header("״̬")]
    public bool isInjure;
    public bool isDie;
    public bool isAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        playAnimation = GetComponent<PlayerAnimation>();

        inputControl = new PlayerInputControl();

        //��Ծ
        inputControl.Gameplay.Jump.started += Jump;

        //����
        inputControl.Gameplay.Attack.started += PlayerAttack;
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
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()//�������йصĶ�����ִ��
    {   
        if(!isInjure && !isAttack)
            Move();   
    }

    public void Move()
    {
        //�����ƶ�
        rb.velocity = new Vector2(inputDirection.x * speed /* Time.deltaTime*/, rb.velocity.y);

        double faceDir = (int)transform.localScale.x;//(��������ʹ�����ڿ��ֵ�ʱ����ʧ��������֪����ô�����

        if (inputDirection.x > 0)
            faceDir = 1;
        if(inputDirection.x < 0)
            faceDir = -1;

        //���﷭ת
        transform.localScale = new Vector3((float)faceDir, 1, 1);
    }


    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if(physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        if (!physicsCheck.isGround)
            return;
        //Debug.Log("attack");
        playAnimation.PlayerAttack();
        isAttack = true;
    }


    public void GetInjure(Transform attacker)
    {
        isInjure = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * injureForce,ForceMode2D.Impulse);
    }

    public void PlayerDie()
    {
        isDie = true;
        inputControl.Gameplay.Disable();
        Invoke("GameoverMenu", 1f);
    }

    private void GameoverMenu()
    {
        SceneManager.LoadScene(2);
    }

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}

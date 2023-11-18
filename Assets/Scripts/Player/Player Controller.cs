using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    public Vector2 inputDirection;
    public PhysicsCheck physicsCheck;
    [Header("��������")]
    public float speed;//�����ƶ�
    public float jumpForce;//��Ծ

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started += Jump;
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
    }

    private void FixedUpdate()//�������йصĶ�����ִ��
    {
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
}


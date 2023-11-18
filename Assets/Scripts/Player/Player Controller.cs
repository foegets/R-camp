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
    [Header("基本参数")]
    public float speed;//左右移动
    public float jumpForce;//跳跃

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

    private void FixedUpdate()//跟物理有关的都在这执行
    {
        Move();   
    }


    public void Move()
    {
        //左右移动
        rb.velocity = new Vector2(inputDirection.x * speed /* Time.deltaTime*/, rb.velocity.y);

        double faceDir = (int)transform.localScale.x;//(这个代码会使人物在开局的时候消失不见，不知道怎么解决）

        if (inputDirection.x > 0)
            faceDir = 1;
        if(inputDirection.x < 0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3((float)faceDir, 1, 1);
    }


    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if(physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }
}


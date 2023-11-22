using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private physicsCheck physicsCheck;

    public Vector2 inputDirection;
    public PlayerinputControl inputControl;
    public Rigidbody2D rb;

    public bool isHurt;
    public bool isDead;

    [Header("基本参数")]
    public float movementSpeed;
    public float jumpForce;
    public float HurtForce;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float jumpSpeed = 5f;
    public float movementForceInAir;

    private void Awake()
    {
        inputControl = new PlayerinputControl();
        physicsCheck = GetComponent<physicsCheck>();

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

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
    }

    //移动
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * movementSpeed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("JUMP");
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            physicsCheck.jumpCount = 1;
            // 物体正在一段跳
            physicsCheck.isJump = true;
        }
        else if (physicsCheck.jumpCount != 0 && !physicsCheck.isGround && physicsCheck.isJump)
        {

            // 再进行一次跳跃操作
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            physicsCheck.jumpCount--;
        }
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * HurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDaead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
}

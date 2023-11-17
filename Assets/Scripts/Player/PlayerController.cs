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

    [Header("基本参数")]
    public float movementSpeed;
    public float jumpForce;
    public float wallCheckDistance;
    public float wallSlideSpeed;
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
        CheckIfWallSliding();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //移动
    public void Move()
    {
        if(physicsCheck.isGround)
        {
            rb.velocity = new Vector2(inputDirection.x * movementSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if(!physicsCheck.isGround && !physicsCheck.isWallSliding && inputDirection.x != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * inputDirection.x, 0);
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * inputDirection.x, rb.velocity.y);
            }
        }

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        if(rb.velocity.x != 0)
        {
            physicsCheck.isWalking = true;
        }
        else
        {
            physicsCheck.isWalking = false;
        }

        //滑墙
        if (physicsCheck.isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }

        //人物翻转
        transform.localScale = new Vector3(faceDir,1,1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("JUMP");
        if (physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    //滑墙
    private void CheckIfWallSliding() 
    {
        if(physicsCheck.isTouchingWall && !physicsCheck.isGround && rb.velocity.y < 0)
        {
            physicsCheck.isWallSliding = true;
        }
        else
        {
            physicsCheck.isWallSliding = false;
        }
    }

}

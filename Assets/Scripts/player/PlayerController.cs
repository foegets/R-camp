using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playeranim;
    private SpriteRenderer spriteRenderer;
  
    public float speed;
    public float jumpForce=16;
    public bool isAttack = true;
    public int combo;
    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();      
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl.Gameplay.Attack.started += Attack;
        playeranim = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        combo = 0;
        physicsCheck.isMovable = true;  
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
        Move();
 
    } 

    public void Move()
    {
        if (physicsCheck.isMovable)
        {
            rb.velocity = new Vector2(inputDirection.x * speed, rb.velocity.y);
        }
        
        float faceDir = transform.localScale.x;

        if (inputDirection.x > 0)
            spriteRenderer.flipX = false;
        else if (inputDirection.x < 0)
            spriteRenderer.flipX= true;
        transform.localScale = new Vector3(faceDir,1,1);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround  && physicsCheck.isJumpable == true)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        playeranim.PlayAttack();
        isAttack = true;
        combo++;
        if (combo >= 2)
        {
            combo = 0;
        }
    }

    


}    


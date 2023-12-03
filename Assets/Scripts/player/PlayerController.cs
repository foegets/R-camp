using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playeranim;
    private SpriteRenderer spriteRenderer;
    private Character character;
  
    public float speed;
    public float jumpForce=16;
    public bool isAttack;
    public float hurtForce;
    public bool isDead;


    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();      
        inputControl = new PlayerInputControl(); 
        physicsCheck = GetComponent<PhysicsCheck>();
        playeranim = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        character = GetComponent<Character>();  
        physicsCheck.isMovable = true;
        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += Attack;
        inputControl.Gameplay.Defend.started += Defend;
        inputControl.Gameplay.Defend.canceled += DefendCanceled;
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
    }

    

    public void GetHurt(Transform attacker)
    {
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(0, 0);
        if (attacker.transform.position.x > transform.position.x)
        {
            dir.x = -1;
        }
        else if (attacker.transform.position.x < transform.position.x)
        {
            dir.x = 1;   
        }
        
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }

    private void Defend(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.velocity = Vector2.zero;
            character.isDefensing = true;
            physicsCheck.isMovable = false;
            physicsCheck.isJumpable = false;
        }   
     }

    private void DefendCanceled(InputAction.CallbackContext context)
    {
        character.isDefensing = false;
        physicsCheck.isMovable = true;
        physicsCheck.isJumpable = true;
    }

 

}


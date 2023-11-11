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
   
    public float speed;
    public float jumpForce=16;
    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();      
        inputControl = new PlayerInputControl();
        inputControl.Gameplay.Jump.started += Jump;
        physicsCheck = GetComponent<PhysicsCheck>();    
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
        rb.velocity = new Vector2(inputDirection.x * speed * Time.fixedDeltaTime, rb.velocity.y);
        float faceDir = transform.localScale.x;
        
        if(inputDirection.x > 0)
            faceDir = 1;
        else if(inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir,1,1);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

}    


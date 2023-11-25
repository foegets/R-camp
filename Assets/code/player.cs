using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;

    [SerializeField] private BoxCollider2D myFeet;

    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }
    void Update()
    {

        AnimatorControllers();
        Movement();
        CheckInput();
       
        CollisionChecks();
        FlipController();


    }
    private void CollisionChecks()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("isGround"));
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
        
    }
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    private void Jump()
    {
        if (isGround)
        {
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = Vector2.up * jumpVel;
        }
    }
    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGround", isGround);


    }
    
   



}
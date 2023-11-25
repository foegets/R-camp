using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{   
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("移动参数")]
    public float speed = 8f;
    public float crouchSpeedDivisor = 8f;

    [Header("跳跃参数")]
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;
    public float crouchJumpBoost = 2.5f;
    public float hangingJumpForce = 15f;

    private float jumpTime;

    [Header("状态")]
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;
    public bool isHeadblocked;
    public bool isHanging;

    [Header("环境检测")]
    public float footOffset = 0.4f;
    public float headClearance = 0.5f;
    public float groundDistance = 0.2f;
    float playerHeight;
    public float eyeHeight = 1.5f;
    public float grabDistance = 0.6f;
    public float reachOffset = 0.7f;

    
    public LayerMask groundLarer;

    public float xVelocity;

    bool jumpPressed;                        //按键设置
    bool jumpHeld;
    bool crouchHeld;
    bool crouchPressed;

    Vector2 colliderStandSize;               //碰撞体尺寸
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        playerHeight = coll.size.y;

        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderCrouchSize = new Vector2(coll.size.x, coll.size.y / 2f);
        colliderCrouchOffset = new Vector2(coll.offset.x, coll.offset.y / 2f);
    }

    void Update()
    {
        if(GameManager.GameOver())
            return;
        
        jumpPressed = Input.GetButton("Jump");               //跳跃
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
        crouchPressed = Input.GetButtonDown("Crouch");
    }

    private void FixedUpdate()
    {
        if (GameManager.GameOver())
            return;
        PhysicsCheck();
        GroundMovement();
        MidAirMovement();
    }

    void PhysicsCheck()
    {   
        //左右脚部射线
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset/2 + 0.1f, 0f), Vector2.down, groundDistance, groundLarer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset/2, 0f), Vector2.down, groundDistance, groundLarer);

        if (leftCheck || rightCheck)
            isOnGround = true;
        else
            isOnGround = false;

        //头部射线
        RaycastHit2D headCheak = Raycast(new Vector2(0f, coll.size.y), Vector2.up, headClearance, groundLarer);

        if (headCheak)
            isHeadblocked = true;
        else isHeadblocked = false;

        float direction = transform.localScale.x;
        Vector2 grabDir = new Vector2(direction, 0f);

        RaycastHit2D blockedCheck = Raycast(new Vector2(footOffset/2 * direction, playerHeight), grabDir, grabDistance, groundLarer);
        RaycastHit2D wallCheck = Raycast(new Vector2(footOffset / 2 * direction, eyeHeight), grabDir, grabDistance-0.1f, groundLarer);
        RaycastHit2D ledgeCheck = Raycast(new Vector2(reachOffset * direction, playerHeight),Vector2.down,grabDistance-0.1f,groundLarer);
    
        if(!isOnGround && rb.velocity.y<0 && ledgeCheck && wallCheck && !isHeadblocked)
        {
            Vector3 pos = transform.position;
            pos.x += wallCheck.distance /*- 0.05f*/ * direction;              //此处取消了吸附效果，防止连续攀附跳后出现悬空攀附的情况
            pos.y -= ledgeCheck.distance;
            transform.position = pos;


            rb.bodyType = RigidbodyType2D.Static;
            isHanging = true;
        }
    
    
    
    }

    void GroundMovement()
    {
        if (isHanging)
            return;

        if (crouchHeld && !isCrouch && isOnGround)
            Crouch();
        else if (!crouchHeld && isCrouch && !isHeadblocked)
            Standup();
        else if (!isOnGround && isCrouch)
            Standup();

        xVelocity = Input.GetAxis("Horizontal");   //-1f  1f  0

        if (isCrouch)
            xVelocity /= crouchSpeedDivisor;

        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

        FlipDirection();
    }

    void MidAirMovement()
    {   
        if(isHanging )
        {
            if (jumpPressed)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(rb.velocity.x, hangingJumpForce);
                isHanging = false;
            }
            if (crouchPressed)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                isHanging = false;
            }

        }


        if (jumpPressed && isOnGround && !isJump && !isHeadblocked && !isHanging)
        {
            if (isCrouch & isOnGround )
            {
                Standup();
                rb.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);

            }
            
            isOnGround = false;
            isJump = true;

            jumpTime = Time.time + jumpHoldDuration;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            AudioManager.PlayJumpAudio();
        }
        else if (isJump)
        {
            if (jumpHeld)
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            if (jumpTime < Time.time)
                isJump = false;
        }


    }
    void FlipDirection()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector3(-1, 1,1);
        if (xVelocity > 0)
            transform.localScale = new Vector3(1, 1,1);
    }

    void Crouch()
    {
        isCrouch = true;
        coll.size = colliderCrouchSize;
        coll.offset = colliderCrouchOffset;
    }

    void Standup()
    {
        isCrouch = false;
        coll.size = colliderStandSize;
        coll.offset = colliderStandOffset;
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDirection * length,color);

        return hit;
    }





}

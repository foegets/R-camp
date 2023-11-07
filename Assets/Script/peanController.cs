using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peanController : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D coll;
    [Header("移动参数")]
    public float speed = 8f;
    private float xVelocity;
    private Vector2 boomDirection;

    [Header("跳跃参数")]
    public float jumpForce = 10f;

    [Header("状态")]
    public bool isOnGround;
    public bool isJump;
    public bool boom;     //触碰地雷了

    [Header("环境检测")]
    public LayerMask platform;
    //按键设置
    private bool jumpPressed;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        jumpPressed = Input.GetButtonDown("Jump");
        Jump();
    }

    private void FixedUpdate()
    {
        IsOnGround();
        if (boom)
        {
            rb.AddForce(boomDirection, ForceMode2D.Impulse);
            boom = false;
        }
    }
    void  Movement()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        turnRound();
    }

    void turnRound()
    {
        if (rb.velocity.x < 0)
            transform.localScale = new Vector2(-1, 1);
        else if (rb.velocity.x > 0)
            transform.localScale = new Vector2(1, 1);
    }

    void IsOnGround()
    {
        if (coll.IsTouchingLayers(platform))
            isOnGround = true;
        else
            isOnGround = false;
    }

    void Jump()
    {
        if(jumpPressed&&isOnGround)
        {
            isOnGround = false;
            isJump = true;
            rb.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.Distance(coll).normal.x+"+"+ collision.Distance(coll).normal.y);
        boomDirection = (collision.Distance(coll).normal)*30;
        boom = true;
    }

}

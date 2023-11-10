using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroMOVE : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] float playerspeed;//速度
    [SerializeField] float jumpspeed; //跳跃速度
    [SerializeField] Transform groudcheck;  //地面检测
    [SerializeField] LayerMask groudlayer;  //层级
    private float horizontal;    //方向
    Vector2 vecGravity;  //当前重力
    [SerializeField] float jumpmultiplier;//跳跃附加速度
    [SerializeField] float fallmuplier; //下落
    [SerializeField] float jumptime;   //跳跃时间
    float jumpcounter;//跳跃持续时间
    bool isjumping;               //是否正在跳跃
    private bool facingright = true;   //判断是否脸超右边
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();        //获取刚体
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");//获取玩家输入的值
        if (Input.GetButtonDown("Jump") && ongroud())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            isjumping = true;
            jumpcounter = 0;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isjumping = false;
        }
        
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if (rb.velocity.y > 0 && isjumping)
        {
            jumpcounter += Time.deltaTime;
            if (jumpcounter > jumptime)
            {
                isjumping = false;
            }
            rb.velocity += vecGravity * jumpmultiplier * Time.deltaTime;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallmuplier * Time.deltaTime;
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(horizontal * playerspeed, rb.velocity.y);
        if ((facingright && horizontal < 0) || (!facingright && horizontal > 0))    //转向
        {
            facingright = !facingright;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }

    private bool ongroud()  //检测在地上
    {
        return Physics2D.OverlapCapsule(groudcheck.position, new Vector2(0.9f, 0.17f), CapsuleDirection2D.Horizontal, 0, groudlayer);
    }

}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
public class player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public bool canJump;
    public Animator animator;
    //设置跳跃参数
    public float jumpParameter = 5.0f;
    //设置移动速度
    public float speed = 5.0f;
    //是否和地雷接触的bool值
    public static bool Contactmine;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //判断是否和平台碰撞
        if (collision.gameObject.CompareTag("platform"))
        {
            canJump = true;
        }
        print(canJump);
    }

    // Update is called once per frame
    void Update()
    {
        //左右移动的参数
        float movex = Input.GetAxisRaw("Horizontal");
        //左右移动的实现
        rb.velocity = new Vector2(movex * speed, rb.velocity.y);
        //移动动画实现
        if (canJump == true && movex != 0)
        {
            //左右放向选择性转换
            if (movex > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movex < 0)
            {
                spriteRenderer.flipX = true;
            }
            print("1");
            animator.SetBool("iswalk", true);
        }
        else
        {
            animator.SetBool("iswalk", false);
        }
        //检测是否符合跳跃标准
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            //跳跃的实现
            rb.velocity = new Vector2(rb.velocity.x, jumpParameter);
            canJump = false;

        }
        //跳跃的动画实现
        if (canJump == false)
        {
            animator.SetBool("isjump", true);
        }
        else
        {
            animator.SetBool("isjump", false);
        }
   
        }
  
    void FixedUpdate()
    {
      
     
    }
    
}

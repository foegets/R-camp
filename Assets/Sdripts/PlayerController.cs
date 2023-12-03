using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    private PauseMune PauseMune;    //引用实例
    private Spikes Spikes;
    private MusicManeger musicManeger;

    public Rigidbody2D theRB;    
    public float Movespeed;//速度
    public float Horizontal;    //方向
    public bool Facingright = true;   //判断是否脸超右边
    public float JumpForce;      //跳跃速度
    public float Speed;//初速度

    public bool isGroud;
    public Transform groudCheck;  //地面检测
    public LayerMask groudLayer;  //层级
    public bool isDoubleJump;

    private Animator anim;

    void Start()
    { 
        anim = GetComponent<Animator>();        //h获取脚本依附对象的Animator
        PauseMune = PauseMune.instance;        //引用实例
        Spikes = Spikes.instance;
        musicManeger = MusicManeger.instance;

    }

    // Update is called once per frame
    void Update()
    { 
        
        if (!PauseMune.instance.isPaused&&!Spikes.instance.isDied)//判断是否在暂停或者死亡
        {
            Horizontal = Input.GetAxisRaw("Horizontal");//获取玩家输入的值
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Movespeed = Speed * 2;
            }
            else
            {
                Movespeed = Speed;
            }
            Move();
            Jump();
        }
        else
        {
            theRB.velocity = new Vector2(0, 0);
        }

        anim.SetBool("isDie", Spikes.instance.isDied);   //动画
        anim.SetBool("isGroud", isGroud);
        anim.SetFloat("moveSpeed",Mathf.Abs(theRB.velocity.x));
        anim.SetFloat("jumpSpeed", theRB.velocity.y);
    }

    
    public void Move()
    {
        theRB.velocity = new Vector2(Horizontal * Movespeed, theRB.velocity.y);         
        
        if ((Facingright && Horizontal < 0) || (!Facingright && Horizontal > 0))    //转向
        {
            Facingright = !Facingright;
            theRB.transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }

    public void Jump()
    {
        isGroud= Physics2D.OverlapCapsule(groudCheck.position, new Vector2(0.75f, 0.14f), CapsuleDirection2D.Horizontal, 0, groudLayer);  //地板判定

        if (isGroud)
        {
            isDoubleJump = true;     
        }

        if (Input.GetButtonUp("Jump"))
        {
            musicManeger.PlaySound(2);//跳跃音效
            if (isGroud)//在地面上跳
            {
                theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
            }
            else
            {
                if (isDoubleJump)  //二段跳
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
                    isDoubleJump = false;
                }
            }
        }
    }

}
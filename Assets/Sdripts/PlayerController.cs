using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    private PauseMune PauseMune;
    private Spikes Spikes;

    // Start is called before the first frame update
    public Rigidbody2D theRB;
    public float Movespeed;//�ٶ�
    public float Horizontal;    //����
    public bool Facingright = true;   //�ж��Ƿ������ұ�
    public float JumpForce;

    public bool isGroud;
    public Transform groudCheck;  //������
    public LayerMask groudLayer;  //�㼶
    public bool isDoubleJump;

    private Animator anim;

    void Start()
    { 
        anim = GetComponent<Animator>();
        PauseMune = PauseMune.instance;
        Spikes = Spikes.instance;
        // theRB = GetComponent<Rigidbody2D>();        //��ȡ����

    }

    // Update is called once per frame
    void Update()
    { 
        
        if (!PauseMune.instance.isPaused&&!Spikes.instance.isDied)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");//��ȡ��������ֵ
            Move();
            Jump();
        }
        anim.SetBool("isDie", Spikes.instance.isDied);
        anim.SetBool("isGroud", isGroud);
       anim.SetFloat("moveSpeed",Mathf.Abs(theRB.velocity.x));
        anim.SetFloat("jumpSpeed", theRB.velocity.y);
    }

    
    public void Move()
    {
        if (!Spikes.instance.isDied)
        {
            theRB.velocity = new Vector2(Horizontal * Movespeed, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(0, 0);
        }
        
        if ((Facingright && Horizontal < 0) || (!Facingright && Horizontal > 0))    //ת��
        {
            Facingright = !Facingright;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }

    public void Jump()
    {
        isGroud= Physics2D.OverlapCapsule(groudCheck.position, new Vector2(0.75f, 0.14f), CapsuleDirection2D.Horizontal, 0, groudLayer);

        if (isGroud)
        {
            isDoubleJump = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (isGroud)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
                
            }
            else
            {
                if (isDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
                    isDoubleJump = false;
                }
            }
        }
     
        
        
    }

}
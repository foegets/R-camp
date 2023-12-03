using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    private PauseMune PauseMune;    //����ʵ��
    private Spikes Spikes;
    private MusicManeger musicManeger;

    public Rigidbody2D theRB;    
    public float Movespeed;//�ٶ�
    public float Horizontal;    //����
    public bool Facingright = true;   //�ж��Ƿ������ұ�
    public float JumpForce;      //��Ծ�ٶ�
    public float Speed;//���ٶ�

    public bool isGroud;
    public Transform groudCheck;  //������
    public LayerMask groudLayer;  //�㼶
    public bool isDoubleJump;

    private Animator anim;

    void Start()
    { 
        anim = GetComponent<Animator>();        //h��ȡ�ű����������Animator
        PauseMune = PauseMune.instance;        //����ʵ��
        Spikes = Spikes.instance;
        musicManeger = MusicManeger.instance;

    }

    // Update is called once per frame
    void Update()
    { 
        
        if (!PauseMune.instance.isPaused&&!Spikes.instance.isDied)//�ж��Ƿ�����ͣ��������
        {
            Horizontal = Input.GetAxisRaw("Horizontal");//��ȡ��������ֵ
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

        anim.SetBool("isDie", Spikes.instance.isDied);   //����
        anim.SetBool("isGroud", isGroud);
        anim.SetFloat("moveSpeed",Mathf.Abs(theRB.velocity.x));
        anim.SetFloat("jumpSpeed", theRB.velocity.y);
    }

    
    public void Move()
    {
        theRB.velocity = new Vector2(Horizontal * Movespeed, theRB.velocity.y);         
        
        if ((Facingright && Horizontal < 0) || (!Facingright && Horizontal > 0))    //ת��
        {
            Facingright = !Facingright;
            theRB.transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }

    public void Jump()
    {
        isGroud= Physics2D.OverlapCapsule(groudCheck.position, new Vector2(0.75f, 0.14f), CapsuleDirection2D.Horizontal, 0, groudLayer);  //�ذ��ж�

        if (isGroud)
        {
            isDoubleJump = true;     
        }

        if (Input.GetButtonUp("Jump"))
        {
            musicManeger.PlaySound(2);//��Ծ��Ч
            if (isGroud)//�ڵ�������
            {
                theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
            }
            else
            {
                if (isDoubleJump)  //������
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
                    isDoubleJump = false;
                }
            }
        }
    }

}
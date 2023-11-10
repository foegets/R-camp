using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroMOVE : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] float playerspeed;//�ٶ�
    [SerializeField] float jumpspeed; //��Ծ�ٶ�
    [SerializeField] Transform groudcheck;  //������
    [SerializeField] LayerMask groudlayer;  //�㼶
    private float horizontal;    //����
    Vector2 vecGravity;  //��ǰ����
    [SerializeField] float jumpmultiplier;//��Ծ�����ٶ�
    [SerializeField] float fallmuplier; //����
    [SerializeField] float jumptime;   //��Ծʱ��
    float jumpcounter;//��Ծ����ʱ��
    bool isjumping;               //�Ƿ�������Ծ
    private bool facingright = true;   //�ж��Ƿ������ұ�
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();        //��ȡ����
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");//��ȡ��������ֵ
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
        if ((facingright && horizontal < 0) || (!facingright && horizontal > 0))    //ת��
        {
            facingright = !facingright;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }

    private bool ongroud()  //����ڵ���
    {
        return Physics2D.OverlapCapsule(groudcheck.position, new Vector2(0.9f, 0.17f), CapsuleDirection2D.Horizontal, 0, groudlayer);
    }

}
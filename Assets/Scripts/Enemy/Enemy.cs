using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //����Ļ�ȡ
    protected Rigidbody2D rb;//�ʼǣ�proteceted���ε���˼�ǽ�1.������Լ�2.��������Է����������

    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("��������")]
    public float normalSpeed=300;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    [Header("��ʱ��")]
    public float waitTime=1;

    public float waitTimeCounter;

    public bool wait;



    private void Awake()
    {//�����Ļ�ȡ
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        physicsCheck= GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x,0,0);//ȡҰ���localscale��x����ĸ�ֵ��y,z��ȡ0
        if (physicsCheck.touchLeftWall&&faceDir.x<0 || physicsCheck.touchRightWall&&faceDir.x>0)
        {
            wait = true;
            anim.SetBool("walk", false);
        }

        TimeCounter();
    }

    private void FixedUpdate()
    {
        Move();
    }
    public virtual void Move()
    {
        //��rb���ٶȱ���ֵΪVector2(x,y)
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }

    public void TimeCounter()//��ʱ��
    {
        if (wait)
        {
            waitTimeCounter-=Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }
}

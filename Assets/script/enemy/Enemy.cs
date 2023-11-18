using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //��protected��֤������������ʣ������Ա��������
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("��������")]
    public float walkingSpeed;
    public float runningSpped;
    public float currentSpped;
    public Vector3 faceDir;
    [Header("��ʱ��")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        if ((physicsCheck.isLeftWall && faceDir.x<0)||(physicsCheck.isRightWall && faceDir.x > 0))
        {
            transform.localScale = new Vector3(faceDir.x, 1, 1);
            anim.SetBool("walk", false);
        }
        TimeCounter();
    }
    private void FixedUpdate()
    {
        Move();
    }

    //virtual��ʾ���⣬�����������н����޸�
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpped * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
            }
        }
    }
}

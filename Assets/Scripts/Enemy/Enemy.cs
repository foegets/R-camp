using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //组件的获取
    protected Rigidbody2D rb;//笔记：proteceted修饰的意思是仅1.这个类以及2.其子类可以访问这个变量

    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("基本参数")]
    public float normalSpeed=300;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    [Header("计时器")]
    public float waitTime=1;

    public float waitTimeCounter;

    public bool wait;



    private void Awake()
    {//变量的获取
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
        physicsCheck= GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x,0,0);//取野猪的localscale的x坐标的负值，y,z都取0
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
        //即rb的速度被赋值为Vector2(x,y)
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }

    public void TimeCounter()//计时器
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

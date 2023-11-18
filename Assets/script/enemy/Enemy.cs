using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //加protected保证不被其他类访问，但可以被子类访问
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("基本参数")]
    public float walkingSpeed;
    public float runningSpped;
    public float currentSpped;
    public Vector3 faceDir;
    [Header("计时器")]
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

    //virtual表示虚拟，可以在子类中进行修改
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

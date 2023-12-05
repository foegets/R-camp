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
    public Transform attacker;//攻击者
    public float hurtForce;
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    [Header("状态")]
    public bool isHurt;


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
        if (!isHurt)
        {
            Move();
        }
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
    
    public void OnTakeDamage(Transform attackTrans)
    {
        //受伤转向
        attacker = attackTrans;
        if (attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        //受伤被击退
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;      
        StartCoroutine(OnHurt(dir));
    }
    //用携程的方式使野猪受击后等待一段时间再将isHurt改为false
    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }


}

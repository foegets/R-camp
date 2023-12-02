using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected Animator anim;
    PhysicsCheck physicsCheck;

    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;
    public Transform attacker;

    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    [Header("状态")]
    public bool isHurt;
    public bool isDie;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDir = new Vector3(transform.localScale.x, 0, 0);

        if ((physicsCheck.touchLeftWall && faceDir.x < 0) || 
            (physicsCheck.touchRightWall && faceDir.x > 0) || 
            !(physicsCheck.isGround))
        {
            wait = true;
            anim.SetBool("Move", false);
        }

        TimeCounter();
    }

    private void FixedUpdate()
    {
        if(!isHurt)
            Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x, rb.velocity.y);
    }


    //受伤计算器
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(-faceDir.x, 1, 1);
                physicsCheck.bottomOffset *= -1; 
            }
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //转身
        if(attackTrans.position.x - transform.position.x > 0)
        {
            //Debug.Log("1");
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(attackTrans.position.x - transform.position.x < 0)
        {
            //Debug.Log("2");
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //受伤被击退
        isHurt = true;
        anim.SetTrigger("injure");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;

        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("die", true);
        isDie = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}

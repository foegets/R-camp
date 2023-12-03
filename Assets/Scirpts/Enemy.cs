using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    public Animator animator;
    [HideInInspector]public PhysicCheck physicCheck;



    [Header("基本参数")]
    public float norlmalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    public Transform attacker;

    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    //private Enums enums;

    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    private baseState currentState;
    protected baseState patrolState;
    protected baseState chaseState;

    [Header("爆金币")]
    public GameObject[] Armor = new GameObject[4];

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicCheck = GetComponent<PhysicCheck>();
        currentSpeed = norlmalSpeed;
        waitTimeCounter = waitTime;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0,0);
        //Debug.Log(faceDir.x);
        currentState.LogicUpdate();
        Timecounter();
    }

   

    private void FixedUpdate()
    {   
        if(!wait&&!isDead&&!isHurt)
        {
            Move();
        }
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    //计时器
    public void Timecounter()
    {
        if (wait)
        {
            animator.SetBool("isWalk", false);
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }

        if(!FoundPlayer())
            {
            lostTimeCounter -= Time.deltaTime;
            }
        else
        {
            lostTimeCounter = lostTime;
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //转身
        if(attackTrans.position.x-transform.position.x>0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        isHurt = true;
        animator.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x-attackTrans.position.x,0).normalized;

        StartCoroutine(Onhurt(dir));
    }

    private IEnumerator Onhurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        isDead = true;
        animator.SetBool("dead",true);
    }

    public bool FoundPlayer()
    {
        return  Physics2D.BoxCast(transform.position + (Vector3)centerOffset,checkSize,0,faceDir,checkDistance,attackLayer);
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        } ;
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset+new Vector3(checkDistance * -transform.localScale.x, 0, 0), 0.2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    [HideInInspector]public PhysicsCheck physicsCheck;
    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector]public float nowSpeed;
    public Vector3 faceDir;
    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    [Header("计时器")]
    public float waitTime = 1.5f;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("状态")]
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        nowSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        currentState.LogicUpdate();
        TimeCounter();
    }
    private void FixedUpdate()
    {
        if (!wait) Move();
        currentState.PhysicsUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    public virtual void Move()

    {

        rb.velocity = new Vector2(nowSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    private void TimeCounter()
    {
        if(wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                anim.SetBool("Walk", true);
            }
        }
        if(!FoundPlayer()&&lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        //else
        //{
        //    lostTimeCounter = lostTime;
        //}
    }
    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
    }
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState=newState;
        currentState.OnEnter(this);
    }
    #region 事件执行方法
    //public void OnTakeDamage(Transform attackTrans)
    //{
    //    attacker= attackTrans;
    //    //转身
    //    if (attackTrans.position.x - transform.position.x > 0)
    //        transform.localScale = new Vector3(-1, 1, 1);
    //    if (attackTrans.position.x - transform.position.x < 0)
    //        transform.localScale = new Vector3(1, 1, 1);
    //    //受伤被击退
    //    isHurt = true;
    //    anim.SetTrigger("Hurt");
    //    Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x) ?;
    //    rb.velocity = new Vector2(0, rb.velocity.y);
    //    StartCoroutine(OnHurt(dir));
    //}
    //private IEnumerator OnHurt(Vector2 dir)
    //{
    //    rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    //    yield return new WaitForSeconds(0.5f);
    //    isHurt = false;
    //}
    #endregion
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance*-transform.localScale.x,0), 0.2f);
    }
}

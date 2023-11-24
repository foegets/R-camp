using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private Vector3 initialPosition;

    /// <summary>
    /// 因为素材妹有跑的动画，只有走的，用的还是骨骼，我不会整，所以以下的walk和run本应该是两个动作和速度，但目前只是一个，包括walk和chase都是一个动作和速度
    /// </summary>

    [Header("各种范围")]
    public float wanderRadius;          //游走半径，移动状态下，如果超出游走半径会返回出生位置
    public float chaseRadius;            //追击半径，当玩家超出追击半径后会放弃追击，返回追击起始位置
    public float defendRadius;          //自卫半径，玩家进入后怪物会追击玩家，当距离<攻击距离则会发动攻击

    [Header("各种参数")]
    public float attackRange;            //攻击距离
    public float walkSpeed;              //移动速度
    public float turnSpeed;              //转身速度
    public float flipThreshold = 0.01f;

    private enum State
    {
        IDLE,      //原地呼吸
        WALK,       //移动
        CHASE,      //追击玩家
        RETURN      //超出追击范围后返回
    }
    private State currentState = State.IDLE;          //默认状态为原地呼吸

    private float diatanceToPlayer;             //怪物与玩家的距离
    private float diatanceToInitial;            //怪物与初始位置的距离
    private Quaternion targetRotation;          //怪物的目标朝向

    private bool isRun = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        //保存初始位置信息
        initialPosition = gameObject.GetComponent<Transform>().position;

    }
    void Update()
    {

        switch (currentState)
        {
            //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
            case State.WALK:                        
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);

                //该状态下的检测指令
                WanderRadiusCheck();
                break;

            //追击状态，朝着玩家跑去(走去)
            case State.CHASE:                       
                if (!isRun)
                {
                    anim.SetTrigger("Walk");
                    isRun = true;
                }
                transform.Translate(Vector2.up * Time.deltaTime * walkSpeed);

                Vector3 direction = (player.transform.position   - transform.position).normalized;

                // 比较方向向量的X值与阈值
                if (direction.x > flipThreshold)
                {
                    // 面向右侧
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else if (direction.x < -flipThreshold)
                {
                    // 面向左侧
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                /*//朝向玩家位置
                targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector2.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);*/

                //该状态下的检测指令
                ChaseRadiusCheck();
                break;

            //返回状态，超出追击范围后返回出生位置
            case State.RETURN:
                //朝向初始位置移动
                targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector2.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                //该状态下的检测指令
                ReturnCheck();
                break;



        }
    }

    //原地呼吸状态的检测
    void DistanceCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (diatanceToPlayer < attackRange)
        {
            anim.SetTrigger("Attack");
        }
        else if (diatanceToPlayer < defendRadius)
        {
            currentState = State.CHASE;
        }

    }


    //游走状态检测，检测敌人距离及游走是否越界
    void WanderRadiusCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        diatanceToInitial = Vector2.Distance(transform.position, initialPosition);

        if (diatanceToPlayer < attackRange)
        {
            anim.SetTrigger("Attack");
        }
        else if (diatanceToPlayer < defendRadius)
        {
            currentState = State.CHASE;
        }

        if (diatanceToInitial > wanderRadius)
        {
            //朝向调整为初始方向
            targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector2.up);
        }
    }

    // 追击状态检测，检测敌人是否进入攻击范围以及是否离开警戒范围
    void ChaseRadiusCheck()
    {
        diatanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        diatanceToInitial = Vector2.Distance(transform.position, initialPosition);

        if (diatanceToPlayer < attackRange)
        {
            anim.SetTrigger("Attack");
        }
        //如果超出追击范围或者敌人的距离超出警戒距离就返回
        if (diatanceToInitial > chaseRadius )
        {
            currentState = State.RETURN;
        }
    }

    //超出追击半径，返回状态的检测，不再检测敌人距离
    void ReturnCheck()
    {
        diatanceToInitial = Vector2.Distance(transform.position, initialPosition);
        //如果已经接近初始位置，则随机一个待机状态
        if (diatanceToInitial < 0.5f)
        {
            isRun = false;
        }
    }


}

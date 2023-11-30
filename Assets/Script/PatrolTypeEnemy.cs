using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTypeEnemy : Enemy_Base
{
    [Header("计时器相关")]
    // 攻击持续时间
    public float AttackLastTime = 2.2f;
    // 仇恨持续时间
    public float AggroLastTime = 3f;
    // 脱战站立持续时间
    public float OutBatElaped = 1f;
    // 巡逻站立持续时间
    public float PatrolElaped = 2.5f;
    // 防御持续时间
    public float DefendLastTime = 3f;
    // 战斗准备时间
    public float BatPrepareTime = 1f;

    [Header("墙壁检测相关")]
    // 设置墙壁检测光线检测距离
    public float RayToMonitorWall_Distance = 10f;
    // 获取检测墙壁层级掩码
    public LayerMask WallLayer;

    [Header("玩家检测相关")]
    // 设置扇形检测区域的角度
    public float MonitorDeg = 120f;
    // 设置扇形检测区域的半径
    public float MonitorRad = 15f;
    // 获取玩家的层级掩码
    public LayerMask PlayerLayer;
    // 获得射线检测对象
    protected RaycastHit hitObject;
    // 检测起始点的Y轴偏移量
    public float Yoffset = 0.5f;

    [Header("对象状态")]
    // 检测是否在巡逻状态
    public bool isonpatrol;
    // 检测前方是否有墙
    public bool isfrontexitwall;
    // 判断是否发现玩家
    public bool isfindplayer;
    // 是否在防御状态
    public bool isdefending;
    // 是否是脱战状态
    public bool isoutbattle;

    // 获得AI相关组件
    NavMeshAgent AiGuide;

    // 协程相关
    // 巡逻相关协程
    protected Coroutine PatrolCoroutine;

    void Start()
    {
        PatrolBaseInitialize();
    }

    
    // 初始化
    protected void PatrolBaseInitialize()
    {
        isonpatrol = true;
        isfrontexitwall = false;
        isfindplayer = false;
        isdefending = false;
        isoutbattle = false;
        AiGuide = GetComponent<NavMeshAgent>();
    }
    // 脱战模式
    protected void OutBattleMode()
    {
        // 设置状态
        SetMoveStatus(false, true, false, false);
        // 回到初始位置
        // 计算对象与玩家之间偏移量
        Vector3 offset = originalpos - transform.position;
        // 计算对象看向玩家的要旋转的角度
        Quaternion targetRot = Quaternion.LookRotation(offset);
        // 使用平滑插值计算当前要旋转的角度
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, smoothing);
        // 朝向初始位置前进
        transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
        if (Vector3.Distance(transform.position, originalpos) <= 0.8f)
        {
            isonpatrol = true;
            transform.position = originalpos;
            transform.rotation = originalrot;
            SetMoveStatus(true, false, false, false);
            StartCoroutine(OutBattleDelayTime());
        }
    }
    // 战斗模式
    protected void BattleMode()
    {
        
        float Distance = Vector3.Distance(transform.position, player.position);
        // 记录当前旋转
        Quaternion currentRot = transform.rotation;

        // 防御姿态
        if (isdefending && !isattacking)
        {
            ElapedTime = Time.time - MarkTimer;
            if (ElapedTime >= DefendLastTime)
            {
                SetMoveStatus(true, false, false, false);
                if (isfindplayer)
                {
                    transform.LookAt(player.position);
                }
                else
                {
                    // 进入准备脱战状态
                    SetModeStatus(false, false, true);
                }
                ElapedTime = 0;
            }
            else
            {
                if (isfindplayer)
                {
                    transform.LookAt(player.position);
                }
            }
            //AiGuide.SetDestination(player.forward);
        }
        else if (Distance <= 5f && !isattacking)
        {
            // 对玩家进行攻击
            transform.LookAt(player.position);
            SetMoveStatus(false, false, true, false);
            MarkTimer = Time.time;
        }
        else if (Distance > 5f && !isattacking)
        {
            // 进入追击状态
            PursueMode(currentRot);
            SetMoveStatus(false, true, false, false);
            //AiGuide.speed = movespeed * 1.2f;
            //AiGuide.SetDestination(player.transform.position);
        }
        // 攻击ing
        else if (isattacking)
        {
            ElapedTime = Time.time - MarkTimer;
            if (ElapedTime >= AttackLastTime)
            {
                // 攻击完检测一次前方是否还有敌人
                // 若没有则进入准备脱战状态
                // 若有则进入防御姿态
                SearchPlayer();
                if (isfindplayer)
                {
                    MarkTimer = Time.time;
                    SetMoveStatus(false, false, false, true);
                }
                else
                {
                    SetModeStatus(false, false, true);
                    SetMoveStatus(true, false, false, false);
                    ElapedTime = 0;
                }
            }
        }

        // 仇恨计时器
        if (!isfindplayer && ElapedTime >= AggroLastTime && !isattacking)
        {
            SetMoveStatus(true, false, false, false);
            SetModeStatus(false, false, true);
            ElapedTime = 0;
        }
    }
    // 受到攻击后的应激反应
    protected void UnderAttack()
    {
        SetModeStatus(false, true, false);
        SetMoveStatus(false, false, false, true);
        MarkTimer = Time.time;
    }
    
    // 巡逻行走
    protected void MoveOnPatrol()
    {   
        transform.position += transform.forward * movespeed * Time.deltaTime;
    }

    #region 间隔时间相关
    // 脱战站立间隔时间
    IEnumerator OutBattleDelayTime()
    {
        yield return OutBatElaped;
        SetModeStatus(true, false, false);
        SetMoveStatus(false, true, false, false);
    }
    // 巡逻站立间隔时间
    IEnumerator PatrolDelayTime()
    {
        yield return PatrolElaped;
        SetMoveStatus(false, true, false, false);
    }
    // 战斗准备时间
    IEnumerator BattleDelayTime()
    {
        yield return BatPrepareTime;
        SetModeStatus(false, true, false);
    }
    #endregion
    #region 设置状态相关
    // 四个动作相关状态
    protected void SetMoveStatus(bool idleActive, bool moveActive, bool attackActive, bool defendActive)
    {
        ismove = moveActive;
        isidle = idleActive;
        isattacking = attackActive;
        isdefending = defendActive;
    }
    // 三个行为模式相关状态
    protected void SetModeStatus(bool patrolActive, bool battleAtive, bool outbatActive)
    {
        isonpatrol = patrolActive;
        isonbattle = battleAtive;
        isoutbattle = outbatActive;
    }
    #endregion

    #region 追击模式
    protected void PursueMode(Quaternion currentRot)
    {
        // 计算对象与玩家之间偏移量
        Vector3 offset = player.position - transform.position;
        // 计算对象看向玩家的要旋转的角度
        Quaternion targetRot = Quaternion.LookRotation(offset);
        // 使用平滑插值计算当前要旋转的角度
        currentRot = Quaternion.Lerp(currentRot, targetRot, smoothing);
        transform.rotation = currentRot;
        // 朝向玩家前进
        transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
    }

    #endregion

    #region 射线检测相关
    // 检测玩家
    protected void SearchPlayer()
    {
        float rightangle = transform.rotation.eulerAngles.y + MonitorDeg / 2;
        float leftangle = transform.rotation.eulerAngles.y - MonitorDeg / 2;
        for (float i = leftangle; i <= rightangle; i += 2)
        {
            Vector3 MonitorDir = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad), transform.position.y + Yoffset, Mathf.Cos(i * Mathf.Deg2Rad));
            if (Physics.Raycast(transform.position + new Vector3(0, Yoffset, 0), MonitorDir, out hitObject, MonitorRad, PlayerLayer))
            {
                if (hitObject.collider.CompareTag("Player"))
                {
                    // 标记时间点
                    MarkTimer = Time.time;
                    // 设置状态
                    isfindplayer = true;
                    isonpatrol = false;
                    // 设置敌人行为
                    transform.LookAt(player.position);
                    SetMoveStatus(true, false, false, false);
                    StartCoroutine(BattleDelayTime());
                    StopCoroutine(PatrolCoroutine);
                }
            }   
        }
        if (hitObject.collider == null)
        {
            isfindplayer = false;
        }
    }
    // 检测墙壁
    protected void DetectWall()
    {
        if(Physics.Raycast(transform.position + new Vector3(0, Yoffset, 0), transform.forward + new Vector3(0, Yoffset, 0), RayToMonitorWall_Distance, WallLayer))
        {
            SetMoveStatus(true, false, false, false);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
            PatrolCoroutine = StartCoroutine(PatrolDelayTime());
        }
    }
    #endregion

}

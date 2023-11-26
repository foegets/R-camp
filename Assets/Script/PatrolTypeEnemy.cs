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

    // 获得AI相关组件
    NavMeshAgent AiGuide; 

    void Start()
    {
        PatrolBaseInitialize();
    }

    
    void Update()
    {

    }
    // 初始化
    protected void PatrolBaseInitialize()
    {
        isonpatrol = true;
        isfrontexitwall = false;
        isfindplayer = false;
        isdefending = false;

        AiGuide = GetComponent<NavMeshAgent>();
    }
    // 脱战模式
    protected void OutBattleMode()
    {
        // 回到初始位置
        // 计算对象与玩家之间偏移量
        Vector3 offset = originalpos - transform.position;
        // 计算对象看向玩家的要旋转的角度
        Quaternion targetRot = Quaternion.LookRotation(offset);
        // 使用平滑插值计算当前要旋转的角度
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, smoothing);
        // 朝向初始位置前进
        transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
        SetBasicStatus(false, true, false);
        if (Vector3.Distance(transform.position, originalpos) <= 0.1f)
        {
            isonpatrol = true;
            transform.position = originalpos;
            transform.rotation = originalrot;
            SetBasicStatus(true, false, false);
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
                isdefending = false;
                if (isfindplayer)
                {
                    transform.LookAt(player.position);
                    StartCoroutine(AttackDelayTime());
                }
                else
                {
                    // 进入准备脱战状态
                    SetBasicStatus(true, false, false);
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
            SetBasicStatus(false, false, false);
            MarkTimer = Time.time;
        }
        else if (Distance > 5f && !isattacking)
        {
            // 进入追击状态
            PursueMode(currentRot);
            SetBasicStatus(false, true, false);
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
                    isdefending = true;
                    MarkTimer = Time.time;
                    SetBasicStatus(false, true, false);
                }
                else
                {
                    SetBasicStatus(true, false, false);
                    ElapedTime = 0;
                }
            }
        }

        // 仇恨计时器
        if (!isfindplayer && ElapedTime >= AggroLastTime && !isattacking)
        {
            SetBasicStatus(true, false, false);
            ElapedTime = 0;
        }
    }
    // 受到攻击后的应激反应
    protected void UnderAttack()
    {
        // 受到攻击后的应激状态
        if (!isonbattle && isgethit && !isdead)
        {
            isonbattle = true;
            isdefending = true;
            MarkTimer = Time.time;
        }
    }
    // 战斗准备
    protected void PrepareBattle()
    {
        transform.LookAt(player.position);
        SetBasicStatus(true, false, false);
        StartCoroutine(AttackDelayTime());
        isonbattle = true;
    }
    // 巡逻
    protected void MoveOnPatrol()
    {
        SetBasicStatus(false, true, false);
        transform.position += transform.forward * movespeed * Time.deltaTime;
    }

    #region 间隔时间相关
    // 攻击间隔时间
    IEnumerator AttackDelayTime()
    {
        yield return AttackElaped;
    }
    // 脱战站立间隔时间
    IEnumerator OutBattleDelayTime()
    {
        yield return OutBatElaped;
    }
    // 巡逻站立间隔时间
    IEnumerator PatrolDelayTime()
    {
        yield return PatrolElaped;
    }
    #endregion
    #region 三个基本状态
    protected void SetBasicStatus(bool idleActive, bool moveActive, bool attackActive)
    {
        ismove = moveActive;
        isidle = idleActive;
        isattacking = attackActive;
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
                    MarkTimer = Time.time;
                    isfindplayer = true;
                    isonpatrol = false;
                }
            }
        }
    }
    // 检测墙壁
    protected void DetectWall()
    {
        if(Physics.Raycast(transform.position + new Vector3(0, Yoffset, 0), transform.forward + new Vector3(0, Yoffset, 0), RayToMonitorWall_Distance, WallLayer))
            {
            SetBasicStatus(true, false, true);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
            StartCoroutine(PatrolDelayTime());
        }
    }
    #endregion

}

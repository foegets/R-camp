using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : Character_BasicStatus
{
    [Header("获取玩家对象")]
    // 获得玩家对象
    public Transform player;

    [Header("计时器相关")]
    public float AttackElaped = 0.5f;

    [Header("对象状态")]
    // 判断是否接近玩家
    public bool iscloseplayer;
    // 判断是否受到攻击
    public bool isgethit;
    // 判断是否在战斗状态
    public bool isonbattle;
    // 判断是否在进行攻击
    public bool isattacking;
    // 判断是否在站立状态
    public bool isidle;
    // 判断是否在移动状态
    public bool ismove;
    void Start()
    {
        EnemyBaseInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void EnemyBaseInitialize()
    {
        isidle = false;
        isonbattle = false;
        iscloseplayer = false;
        isgethit = false;
        isattacking = false;
        ismove = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy_StatusMonitor : MonoBehaviour
{
    [Header("获取玩家对象")]
    // 获得玩家对象
    public Transform player;
    // 获得动画机
    Animator animator;
    [Header("血条相关")]
    // 获得血条
    public UnityEngine.UI.Slider HP_Bar;
    public float HP = 100;
    [Header("墙壁检测相关")]
    // 设置墙壁检测光线检测距离
    public float RayToMonitorWall_Distance = 10f;
    // 获取检测墙壁层级掩码
    public LayerMask WallLayer;
    // 获得射线检测对象
    RaycastHit hitObject;
    [Header("速度相关")]
    // 移动速度
    public float movespeed = 10f;
    
    
    [Header("玩家检测相关")]
    // 设置扇形检测区域的角度
    public float MonitorDeg = 120f;
    // 设置扇形检测区域的半径
    public float MonitorRad = 15f;
    // 获取玩家的层级掩码
    public LayerMask PlayerLayer;
       
    // 计时器
    float Timer;
    [Header("间隔时间")]
    // 间隔时间
    public float ElapedTime;

    // 获得初始位置
    Vector3 originalpos;
    [Header("对象状态")] 
    // 判断是否死亡
    public bool isdead;
    // 判断是否在战斗状态
    public bool isonbattle;
    // 检测是否在巡逻状态
    public bool isonpatrol;
    // 检测前方是否有墙
    public bool isfrontexitwall;
    // 判断是否发现玩家
    public bool isfindplayer;
    // 判断是否接近玩家
    public bool iscloseplayer;
    // 判断是否受到攻击
    public bool isgethit;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        originalpos = transform.position;
        HP_Bar.maxValue = HP;
        HP_Bar.minValue = 0;
        HP_Bar.value = HP;
        isdead = false;
        isonpatrol = true;
        isonbattle = false;
        isfrontexitwall = false;
        Timer = 0f;
        ElapedTime = 0f;
        isfindplayer = false;
        iscloseplayer = false;
        isgethit = false;
    }

    private void FixedUpdate()
    {
       
    }
    
    void Update()
    {
        
        if (HP_Bar.value == 0)
        {
            isdead = true;
            animator.SetTrigger("isdead");
        }
        // 攻击ing
        if (isonbattle && iscloseplayer)
        {
            ElapedTime = Time.time - Timer;
            if (ElapedTime >= 2.2f)
            {
                iscloseplayer = false;
                ElapedTime = 0;
                animator.SetBool("iscloseplayer", false);
                animator.SetBool("isfindplayer", false);
            }
        }
        // 检测前方扇形区域是否有玩家
        float rightangle = transform.rotation.eulerAngles.y + MonitorDeg / 2;
        float leftangle = transform.rotation.eulerAngles.y - MonitorDeg / 2;
        for (float i = leftangle; i <= rightangle; i+= 2)
        {
            Vector3 MonitorDir = new Vector3(Mathf.Sin(i * Mathf.Deg2Rad), transform.position.y + 0.5f, Mathf.Cos(i * Mathf.Deg2Rad));
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), MonitorDir, out hitObject, MonitorRad, PlayerLayer))
            {
                if (hitObject.collider.CompareTag("Player"))
                {
                    Timer = Time.time;
                    isfindplayer = true;
                    isonbattle = true;
                    isonpatrol = false;
                }
            }
            
        }
        if (isgethit)
        {
            Timer = Time.time;
            isfindplayer = true;
            isonpatrol = false;
            isonbattle = true;
        }
        // 设置追击时间
        if (isfindplayer)
        {
            if (ElapedTime >= 4f && !iscloseplayer)
            {
                animator.SetBool("isfindplayer", false);
                animator.SetBool("iscloseplayer", false);
                isonpatrol = true;
                isonbattle = false;
                isfindplayer = false;
                transform.position = originalpos;
                transform.eulerAngles = new Vector3(0, 90, 0);
                ElapedTime = 0;
            }
        }
        // 检测前方是否有障碍物
        // 设置墙壁检测光线
        Ray DistanceMonitorRay = new Ray(transform.position + Vector3.up, transform.forward);
        // 发射光线
        isfrontexitwall = Physics.Raycast(DistanceMonitorRay, RayToMonitorWall_Distance, WallLayer);
        // 站立模式
        if (!isonpatrol && !isdead && !isonbattle)
        {
            animator.SetBool("ismove_patrol", false);
            ElapedTime = Time.time - Timer;
            if (ElapedTime > 3.5f)
            {
                isonpatrol = true;
                ElapedTime = 0;
            }
        }
        // 巡逻模式
        if (isonpatrol && !isdead)
        {
            Timer = Time.time;
            transform.position += transform.forward * movespeed * Time.deltaTime;
            animator.SetBool("ismove_patrol", true);
            if (isfrontexitwall)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
                isonpatrol = false;
            }
        }
        // 战斗模式
        else if (isonbattle && !isdead && !iscloseplayer)
        {
            animator.SetBool("isfindplayer", true);
            ElapedTime = Time.time - Timer;
            transform.LookAt(player.position);
            transform.position += transform.forward * movespeed * 1.2f * Time.deltaTime;
            if (Vector3.Distance(player.position, transform.position) <= 4f)
            {
                Timer = Time.time;
                iscloseplayer = true;
                animator.SetBool("iscloseplayer", true);
            }
            else
            {
                animator.SetBool("iscloseplayer", false);
            }
        }
    }
}

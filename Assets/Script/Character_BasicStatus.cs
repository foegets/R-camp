using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_BasicStatus : MonoBehaviour
{
    [Header("血条相关")]
    // 获得血条
    public UnityEngine.UI.Slider HP_Bar;
    // 设置最大血量
    public float HP = 100;

    // 获得动画机
    protected Animator animator;

    [Header("速度相关")]
    // 移动速度
    public float movespeed = 8f;
    // 旋转速度
    public float rotatespeed = 15f;
    // 平滑插值
    public float smoothing = 0.2f;

    [Header("计时器相关")]
    // 间隔时间
    public float ElapedTime;
    // 标记时间点
    public float MarkTimer;

    // 获得初始位置
    protected Vector3 originalpos;
    // 获得初始旋转
    protected Quaternion originalrot;
    // 获得初始缩放
    protected Vector3 originalscale;

    [Header("对象状态")]
    // 判断是否死亡
    public bool isdead;
    void Start()
    {
        CharacterBaseInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        // 检测死亡
        DeathDetect();
    }

    protected void CharacterBaseInitialize()
    {
        isdead = false;

        animator = GetComponent<Animator>();

        originalpos = transform.position;
        originalrot = transform.rotation;
        originalscale = transform.localScale;

        HP_Bar.maxValue = HP;
        HP_Bar.minValue = 0;
        HP_Bar.value = HP_Bar.maxValue;

        MarkTimer = 0f;
        ElapedTime = 0f;
    }

    protected void DeathDetect()
    {
        if (HP_Bar.value == HP_Bar.minValue)
        {
            isdead = true;
            animator.SetTrigger("isdead");
        }
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Attribute : MonoBehaviour
{   [Header("基本属性")]
    public  int Hp;
    [SerializeField] float currentHp;
    [Header("无敌帧")]
    public  float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public Text HP;
    public Rigidbody2D body;
    public UnityEvent<Transform> OnTakeDamage;
    public Animator anim;

    private void Start()
    {
        currentHp = Hp;
        DeadAnimation();
    }
    private void Update()
    {
        if (invulnerable)
        { 
            invulnerableCounter -= Time.deltaTime;//如果进入了无敌帧 开始倒计时
        if(invulnerableCounter <= 0)
        { 
                invulnerable = false;//如果倒计时结束 无敌帧也结束 为什么小于等于0呢，是因为这是Unity引擎
        }
            DeadAnimation();
    }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart")
        {
            currentHp += 5;
            Destroy(collision.gameObject);
        }

    }
    public void TakeDamage(Attack attacker)//用来接受伤害并计算的函数
    {
        
        if (invulnerable==true)//如果处于无敌帧 将不会扣血
        return;
        if (currentHp - attacker.damage >= 0)//为防止血条变负数
        {
            currentHp -= attacker.damage;//掉血
            TriggerInvulnerable();//掉血后，让后让玩家重新进入无敌帧状态
            OnTakeDamage?.Invoke(attacker.transform);
            UpdateHp();
        }
        else
        {
            currentHp = 0;
        }
    }
    
    private void TriggerInvulnerable()
    {
        if(!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;//重新倒计时
        }
    }
    private void UpdateHp()
    {
        HP.text = "HP:" + currentHp;
    }
   

        public void DeadAnimation()
    {
        anim.SetFloat("HP", currentHp);
    }
}

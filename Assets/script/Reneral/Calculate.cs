using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//该代码用来计算人物敌人血量等属性变化
public class Calculate : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    
    [Header("受伤无敌时间")]
    public float invulneraleDuration;
    //创建一个计时器用来计时受伤后短暂的无敌时间,用private可以使其不会出现在inspect中
    private float invulnerableCounter;
    public bool invulnerable;
    
    //建立OnTakeDamage事件并传入Tansform组件来实现受伤击退，注意开头using
    public UnityEvent<Transform> OnTakeDamage;
    //建立OnDie事件在血量见底时执行死亡
    public UnityEvent OnDie;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    //计时器获值并开始计时，（Time.deltaTime即完成上一帧所用的时间，update以帧的方式实现更新？）
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)//计时器速度过快可能不会直接等于0因此用<=
            {
                invulnerable = false;
            }
        }
    }


    //接收攻击者的攻击伤害
    public void TakeDamage(Attack attacker)
    {
        //先判断是否无敌，若为true则代码停止，不再进行伤害计算
        if (invulnerable)
            return;
        //再判断当前血量是否够减，不够则直接等于0,将所有有关受伤的内容传入OnTakeDamage执行
        if(currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //启动Invoke事件OnTakeDamage调用其中的内容,注意前面建立事件时需要传入transform
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //判断并触发死亡
            OnDie?.Invoke();
        }

    }
    
    //开启无敌并给计时器赋值
    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulneraleDuration;
        } 
    }
}

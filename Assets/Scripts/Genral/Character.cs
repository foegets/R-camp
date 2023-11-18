using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    //基本属性
    public float maxHealth;
    public float currentHealth;


    public float invulnerableDuration;//无敌时间 计时器算法
    public float invulnerableCounter;
    public bool invulnerable;
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;//持续不断的减去时间修正直到小于等于0
            if (invulnerableCounter <= 0) //超过0会变成负数的小数
            {
                invulnerable = false;
            }
        }    
    }


    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;//一旦条件成立会触发 
        }
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;//执行受伤
            TrigggerInvlnerable();
        }
        else
        {
            currentHealth = 0;//触发死亡。避免负数情况
            OnDie?.Invoke();        
        }
        OnHealthChange?.Invoke(this);
        //每次受伤都会得到血量的变更
        // other.GetComponent<Character>().TakeDamage(this);
        currentHealth -= attacker.damage;
        TrigggerInvlnerable();
    }
    private void TrigggerInvlnerable()
    {
        invulnerable = true;
        invulnerableCounter = invulnerableDuration;
    }
}

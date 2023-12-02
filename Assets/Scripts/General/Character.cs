using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public Item armor;
    public bool isArmorUsed;
    [Header("事件监听")]
    public VoidEventSO newGameEvent;
    [Header("基本属性")]
    public float maxHealth;
    public float nowHealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    private void Start()
    {
        nowHealth = maxHealth;
    }
    private void NewGame()
    {
        nowHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;
    }
    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0) 
            {
                invulnerable = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Blood")) // 如果碰撞的游戏对象tag为"Blood"
        {
            nowHealth += 1f;
            if (nowHealth >= maxHealth) nowHealth = maxHealth;
            OnHealthChange?.Invoke(this);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Water"))
        {
            //死亡，更新血量
            nowHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)  return;
        // 检查attacker的标签是否为"Enemy"，同时检查背包是否有盾牌
        if (attacker.CompareTag("Enemy") && armor.itemHeld!=0 && isArmorUsed==false)
        {
            attacker.damage /= 2;
            isArmorUsed=true;
        }
        if (nowHealth - attacker.damage > 0)
        {
            nowHealth -= attacker.damage;
            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);
            TriggerInvulnerable();
        }
        else
        {
            nowHealth = 0;//触发死亡
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }
    /// <summary>
    /// 触发受伤无敌
    /// </summary>
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}

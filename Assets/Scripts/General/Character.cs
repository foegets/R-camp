using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("�¼�����")]
    public VoidEventSO newGameEvent;
    [Header("��������")]
    public float maxHealth;
    public float nowHealth;

    [Header("�����޵�")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Water"))
        {
            //����������Ѫ��
            nowHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)  return;
        if(nowHealth - attacker.damage > 0)
        {
            nowHealth -= attacker.damage;
            OnTakeDamage?.Invoke(attacker.transform);
            TriggerInvulnerable();
        }
        else
        {
            nowHealth = 0;//��������
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }
    /// <summary>
    /// ���������޵�
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public Item armor;
    public bool isArmorUsed;
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
        if (other.gameObject.CompareTag("Blood")) // �����ײ����Ϸ����tagΪ"Blood"
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
            //����������Ѫ��
            nowHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)  return;
        // ���attacker�ı�ǩ�Ƿ�Ϊ"Enemy"��ͬʱ��鱳���Ƿ��ж���
        if (attacker.CompareTag("Enemy") && armor.itemHeld!=0 && isArmorUsed==false)
        {
            attacker.damage /= 2;
            isArmorUsed=true;
        }
        if (nowHealth - attacker.damage > 0)
        {
            nowHealth -= attacker.damage;
            //ִ������
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

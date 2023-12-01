using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]

    public float maxHealth;

    public float currentHealth;

    [Header("�����޵�")]

    public float invulnerableDuration;

    private float invulnerableCounter;

    public bool invulnerable;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie ;
    public void Start()
    {
        currentHealth = maxHealth;
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
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)  
            return;

        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //ִ������
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //��������
            OnDie?.Invoke();
        }
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable)  
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
    
}

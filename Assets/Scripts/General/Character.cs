using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int maxStamina;
    public int stamina;
    public int maxMagicka;
    public int magicka;

    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool isInvulnerable;
    
    public bool isDefensing;
    public bool isAttacking;
    public bool isUnderAttack;
    public bool isDead;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDead;
    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (isInvulnerable)
        {
           
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                isUnderAttack = false;
                isInvulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
       if(isInvulnerable)
            return;
        if (isDefensing)
        {
            return ;
        }
        else
        {
            if (health - attacker.damage > 0)
            {
                isUnderAttack = true;
                health -= attacker.damage;
                TriggerInvulnerable();
                OnTakeDamage?.Invoke(attacker.transform);
            }
            else
            {
                isUnderAttack = true;
                health = 0;
                isDead = true;
                OnDead?.Invoke();

            }
        }
    }

    public void TriggerInvulnerable()
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

    public void ConsumeStamina()
    {

    }

   
}

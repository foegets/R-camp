using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;

    public float currentHealth;

    [Header("无敌帧")]
    public float invulnerableDuration;

    private float invulnerableCounter;

    public bool invulnerable;
    private void Start()
    {
        currentHealth = maxHealth;
        invulnerable = false;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0 )
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable==true)
            return;

        if (currentHealth - attacker.damage > 0)
        {
            Debug.Log(attacker.damage);
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
        }
        else
            currentHealth = 0;//death

    }

    private void TriggerInvulnerable()
    {
        if (invulnerable != true)
            invulnerable = true;

        invulnerableCounter = invulnerableDuration;
    }
   
}

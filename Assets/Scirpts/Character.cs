using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityEvent<Character> OnHealthChange;
    [Header("基本属性")]
    public float MaxHealth;
    public float CurrentHealth;

    [Header("无敌属性")]
    public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Transform> Ontakedamage;
    public UnityEvent Dead;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this);
    }

    private void Update()
    {
        if (invulnerable)
        { 
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamege(Attack attacker)
    {
        if (invulnerable) { return; }

        if (CurrentHealth -attacker.damage> 0) 
        {
            CurrentHealth -= attacker.damage;
            TriggerInvulnerable();
            //执行受伤
            Ontakedamage?.Invoke(attacker.transform);
            //Debug.Log("igethurt");
        }
        else 
        {
            CurrentHealth = 0;
            //触发死亡
            Dead?.Invoke();
        }
        OnHealthChange?.Invoke(this);

    }


    private void TriggerInvulnerable()
    { 
        if(!invulnerable) 
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}

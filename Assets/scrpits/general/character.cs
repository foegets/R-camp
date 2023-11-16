using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基本属性")]
    public float maxhealth;
    public float currenthealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    private bool invulnerable;
    private void Start()
    {
        currenthealth = maxhealth;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter-=Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }
    public void Takedamage(attack attacker)
    {
        if (invulnerable)
            return;
        if (currenthealth - attacker.damage > 0)
        {
            currenthealth -= attacker.damage;
            TriggerInvulnerable();
        }
        else { currenthealth=0; }
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


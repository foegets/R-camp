using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class character : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基本属性")]
    public float maxhealth;
    public float currenthealth;
    public float starthealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    private bool invulnerable;


    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent Ondie;



    private void Start()
    {
        currenthealth = starthealth;
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
            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);//invoke启动事件内容
        }
        if (invulnerable)
            return;
        if (currenthealth - attacker.damage <= 0&&currenthealth>0)
        {
            currenthealth = 0;
       
            Ondie?.Invoke();

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
    public void dragon()
    {
        currenthealth = maxhealth;
    }
}


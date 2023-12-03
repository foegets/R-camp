using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour
{

    public UnityEvent<Character> OnHealthChange;//使用UnityEvent的方式进行广播
    [Header("基本属性")]

    public float maxHealth=100;

    public float currentHealth;

    [Header("受伤后无敌")]
    public float invulnerableDuration;

    public bool invulnerable;

    private float invulnerableCounter;

    public UnityEvent<Transform> OnTakeDamage;//传入了一个transform的参数
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
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
        if(transform.position.y < -11.5)
        {
            currentHealth = 0;

        }
    }

    public void TakeDamage(Attack attacker/*Attack类型的变量，类似于int a*/){
        if(invulnerable) 
            return;
        if(currentHealth-attacker.damage > 0) {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();//触发无敌状态
            /*接下来执行受伤*/
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;//不触发无敌，进入死亡状态
            OnDie?.Invoke();/*触发死亡的函数*/
        }

        OnHealthChange?.Invoke(this);//受伤之后，将血量传递出去
    }

    private void TriggerInvulnerable()//触发无敌状态
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}

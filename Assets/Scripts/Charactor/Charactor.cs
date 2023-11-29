using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Charactor : MonoBehaviour
{
    public float health;
    public float involnerableTime;//设置的无敌时间
    private float involnerableCounter;//无敌时间计数器
    public bool involnerable;
    public UnityEvent<Transform> OnTakeDamage;//受伤时候触发的事件，传入Transform用于得知人物受攻击方向
    public UnityEvent OnBeingDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(involnerable)
        {
            involnerableCounter -= Time.deltaTime;//无敌时间计数器减小
            if(involnerableCounter <= 0 )
                involnerable = false;
        }
    }
    public void TakeDamage(Attack attacker)//角色受伤，若处于无敌不执行
    {
        if (involnerable)
            return;
        if (health - attacker.damage <= 0)
        {
            health = 0;
            OnBeingDead?.Invoke();
        }
        else
        {
            health -= attacker.damage;
            TriggerInvolnerable();
            OnTakeDamage?.Invoke(attacker.transform);//传入攻击者位置
        }
    }
    private void TriggerInvolnerable()//控制角色是否进入无敌
    {
        if(!involnerable)
        {
            involnerable = true;
            involnerableCounter = involnerableTime;
        }
    }
}

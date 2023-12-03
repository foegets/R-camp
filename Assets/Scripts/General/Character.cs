using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour
{

    public UnityEvent<Character> OnHealthChange;//ʹ��UnityEvent�ķ�ʽ���й㲥
    [Header("��������")]

    public float maxHealth=100;

    public float currentHealth;

    [Header("���˺��޵�")]
    public float invulnerableDuration;

    public bool invulnerable;

    private float invulnerableCounter;

    public UnityEvent<Transform> OnTakeDamage;//������һ��transform�Ĳ���
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

    public void TakeDamage(Attack attacker/*Attack���͵ı�����������int a*/){
        if(invulnerable) 
            return;
        if(currentHealth-attacker.damage > 0) {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();//�����޵�״̬
            /*������ִ������*/
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;//�������޵У���������״̬
            OnDie?.Invoke();/*���������ĺ���*/
        }

        OnHealthChange?.Invoke(this);//����֮�󣬽�Ѫ�����ݳ�ȥ
    }

    private void TriggerInvulnerable()//�����޵�״̬
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}

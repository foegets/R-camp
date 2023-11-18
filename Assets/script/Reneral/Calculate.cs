using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//�ô������������������Ѫ�������Ա仯
public class Calculate : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;
    
    [Header("�����޵�ʱ��")]
    public float invulneraleDuration;
    //����һ����ʱ��������ʱ���˺���ݵ��޵�ʱ��,��private����ʹ�䲻�������inspect��
    private float invulnerableCounter;
    public bool invulnerable;
    
    //����OnTakeDamage�¼�������Tansform�����ʵ�����˻��ˣ�ע�⿪ͷusing
    public UnityEvent<Transform> OnTakeDamage;
    //����OnDie�¼���Ѫ������ʱִ������
    public UnityEvent OnDie;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    //��ʱ����ֵ����ʼ��ʱ����Time.deltaTime�������һ֡���õ�ʱ�䣬update��֡�ķ�ʽʵ�ָ��£���
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)//��ʱ���ٶȹ�����ܲ���ֱ�ӵ���0�����<=
            {
                invulnerable = false;
            }
        }
    }


    //���չ����ߵĹ����˺�
    public void TakeDamage(Attack attacker)
    {
        //���ж��Ƿ��޵У���Ϊtrue�����ֹͣ�����ٽ����˺�����
        if (invulnerable)
            return;
        //���жϵ�ǰѪ���Ƿ񹻼���������ֱ�ӵ���0,�������й����˵����ݴ���OnTakeDamageִ��
        if(currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //����Invoke�¼�OnTakeDamage�������е�����,ע��ǰ�潨���¼�ʱ��Ҫ����transform
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //�жϲ���������
            OnDie?.Invoke();
        }

    }
    
    //�����޵в�����ʱ����ֵ
    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulneraleDuration;
        } 
    }
}

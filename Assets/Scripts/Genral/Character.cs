using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    //��������
    public float maxHealth;
    public float currentHealth;


    public float invulnerableDuration;//�޵�ʱ�� ��ʱ���㷨
    public float invulnerableCounter;
    public bool invulnerable;
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
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
            invulnerableCounter -= Time.deltaTime;//�������ϵļ�ȥʱ������ֱ��С�ڵ���0
            if (invulnerableCounter <= 0) //����0���ɸ�����С��
            {
                invulnerable = false;
            }
        }    
    }


    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;//һ�����������ᴥ�� 
        }
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;//ִ������
            TrigggerInvlnerable();
        }
        else
        {
            currentHealth = 0;//�������������⸺�����
            OnDie?.Invoke();        
        }
        OnHealthChange?.Invoke(this);
        //ÿ�����˶���õ�Ѫ���ı��
        // other.GetComponent<Character>().TakeDamage(this);
        currentHealth -= attacker.damage;
        TrigggerInvlnerable();
    }
    private void TrigggerInvlnerable()
    {
        invulnerable = true;
        invulnerableCounter = invulnerableDuration;
    }
}

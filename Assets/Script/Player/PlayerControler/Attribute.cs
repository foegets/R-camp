using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Attribute : MonoBehaviour
{   [Header("��������")]
    public  int Hp;
    [SerializeField] float currentHp;
    [Header("�޵�֡")]
    public  float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public Text HP;
    public Rigidbody2D body;
    public UnityEvent<Transform> OnTakeDamage;
    public Animator anim;

    private void Start()
    {
        currentHp = Hp;
        DeadAnimation();
    }
    private void Update()
    {
        if (invulnerable)
        { 
            invulnerableCounter -= Time.deltaTime;//����������޵�֡ ��ʼ����ʱ
        if(invulnerableCounter <= 0)
        { 
                invulnerable = false;//�������ʱ���� �޵�֡Ҳ���� ΪʲôС�ڵ���0�أ�����Ϊ����Unity����
        }
            DeadAnimation();
    }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart")
        {
            currentHp += 5;
            Destroy(collision.gameObject);
        }

    }
    public void TakeDamage(Attack attacker)//���������˺�������ĺ���
    {
        
        if (invulnerable==true)//��������޵�֡ �������Ѫ
        return;
        if (currentHp - attacker.damage >= 0)//Ϊ��ֹѪ���为��
        {
            currentHp -= attacker.damage;//��Ѫ
            TriggerInvulnerable();//��Ѫ���ú���������½����޵�֡״̬
            OnTakeDamage?.Invoke(attacker.transform);
            UpdateHp();
        }
        else
        {
            currentHp = 0;
        }
    }
    
    private void TriggerInvulnerable()
    {
        if(!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;//���µ���ʱ
        }
    }
    private void UpdateHp()
    {
        HP.text = "HP:" + currentHp;
    }
   

        public void DeadAnimation()
    {
        anim.SetFloat("HP", currentHp);
    }
}

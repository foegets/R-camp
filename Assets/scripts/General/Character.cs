using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;

    public float currentHealth;

    [Header("�޵�֡")]
    public float invulnerableDuration;

    public bool invulnerable;
    [Header("״̬")]
    public bool isHurt;

    public bool isdead;

    private float invulnerableCounter;

    private PhysicsCheck PhysicsCheck;

    public PlayerController playerController;


    [Header("�¼�")]
    public UnityEvent<Transform> OntakeDamage;

    public UnityEvent OnDie;

    private void Awake()
    {
        PhysicsCheck = GetComponent<PhysicsCheck>();

        playerController = GetComponent<PlayerController>();
    }
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
            isHurt = true;

            Debug.Log(attacker.damage);
            currentHealth -= attacker.damage;
            TriggerInvulnerable();

            OntakeDamage?.Invoke(attacker.transform);

        }
        else
        {
            currentHealth = 0;//death
            OnDie?.Invoke();
        }


    }//����ܻ�

    public void TakeDamage(RemoteWeapon attacker)
    {
        if (invulnerable == true)
            return;

        if (currentHealth - attacker.weaponDamage > 0)
        {
            isHurt = true;

            Debug.Log(attacker.weaponDamage);
            currentHealth -= attacker.weaponDamage;
            TriggerInvulnerable();

            //OntakeDamage?.Invoke(attacker.transform);

        }
        else
        {
            currentHealth = 0;//death
            OnDie?.Invoke();
        }


    }//����Զ���ܻ�
    private void TriggerInvulnerable()
    {
        if (invulnerable != true)
            invulnerable = true;

        invulnerableCounter = invulnerableDuration;
    }

    public void PlayerDead()
    {
        isdead = true;

        playerController.inputControl.gamePlayer.Disable();
    }//����

    public void OtherDead()
    {
        isdead = true;
        Destroy(gameObject, 10);
    }
   
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;

    public float currentHealth;

    [Header("无敌帧")]
    public float invulnerableDuration;

    public bool invulnerable;
    [Header("状态")]
    public bool isHurt;

    public bool isdead;

    private float invulnerableCounter;

    private PhysicsCheck PhysicsCheck;

    public PlayerController playerController;


    [Header("事件")]
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


    }//玩家受击

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


    }//其他远程受击
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
    }//死亡

    public void OtherDead()
    {
        isdead = true;
        Destroy(gameObject, 10);
    }
   
}

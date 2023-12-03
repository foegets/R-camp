using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

public class RemoteWeapon : Weapon
{
    [Header("基本参数")]
    public float attackDistance;

    public float flySpeed;

    public float checkRudius;

    public float exitTime;


    [HideInInspector] public Vector3 fireDir;

    public LayerMask LayerMask;

    protected float angle;

    protected override void Awake()
    {
        base.Awake();

        fireDir = GameObject.Find("player").GetComponent<PlayerController>().fireDir;

        Debug.Log("remoteWeaon Awake");

        angle = Mathf.Atan2(fireDir.y, fireDir.x);
        Debug.Log(angle);

        rb.rotation = angle*Mathf.Rad2Deg;

        rb.velocity = fireDir*flySpeed;
    }

    protected override  void Update()
    {
        hit();
    }

    public virtual Vector3 summonpos(GameObject player)
    {
        Vector3 summonpos = player.transform.position + new Vector3(0,1.5f,0);
        //Debug.Log(firepos+"firepos");
        //Debug.Log("RemoteWeapon Fire");
        //Debug.Log(player.gameObject.name+" "+"GameObject");
        //Instantiate(PreFab,firepos,quaternion);

        return summonpos;
    }

    protected virtual void GetFireDir(GameObject player)
    {
        fireDir = player.GetComponent<PlayerController>().fireDir;
    }

    protected virtual void hit()
    {
        Collider2D enterCollider =  AttackCheck();
        if (enterCollider?.tag == "Ground"||enterCollider?.tag == "Playform")
        {
            rb.velocity = new Vector3(0,0,0);
            animator.SetTrigger("Hit");
            Destroy(gameObject, exitTime);
        }

        else if (enterCollider?.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
        }

        else if(enterCollider?.tag == "Enemy")
        {
            //attack
            Character beAttacker = enterCollider.GetComponent<Character>();
            beAttacker.TakeDamage(this);

            rb.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("Hit");
            Destroy(gameObject, exitTime);
        }

        else
        {
            GetComponent<Collider2D>().enabled = true;
            Destroy(gameObject, 10);
        }
    }

    protected virtual Collider2D AttackCheck()
    {
        //Collider2D enterCollider = Physics2D.OverlapCircle((Vector2)transform.position, checkRudius, LayerMask);
        Collider2D enterCollider = Physics2D.OverlapCapsule((Vector2)transform.position+new Vector2(0.25f,-0.05f), new Vector2(3f,1f),CapsuleDirection2D.Horizontal,0, LayerMask);
        return enterCollider;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position,checkRudius);
    }
}




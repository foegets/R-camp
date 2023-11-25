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


    private Vector3 fireDir;

    private Rigidbody2D rb;

    private new Collider2D collider2D;

    private Animator animator;


    public LayerMask LayerMask;

    public GameObject PreFab;

    private float angle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        fireDir = GameObject.Find("player").GetComponent<PlayerController>().fireDir;
        Debug.Log(fireDir + "In remoteweapon");

        angle = Mathf.Atan2(fireDir.y, fireDir.x);

        //transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        transform.rotation = Quaternion.EulerRotation(0, 0, angle);
        rb.velocity = fireDir*flySpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit();
    }

    private void FixedUpdate()
    {
        
    }

    public void Fire(GameObject player)
    {
        Vector3 firepos = player.transform.position + new Vector3(0,1.5f,0);

        quaternion quaternion = player.transform.rotation;

        Debug.Log(firepos+"firepos");
        Debug.Log("RemoteWeapon Fire");
        Debug.Log(player.gameObject.name+" "+"GameObject");

        Instantiate(PreFab,firepos,quaternion);
    }

    public Collider2D AttackCheck()
    {
        //Collider2D enterCollider = Physics2D.OverlapCircle((Vector2)transform.position, checkRudius, LayerMask);
        Collider2D enterCollider = Physics2D.OverlapCapsule((Vector2)transform.position+new Vector2(0.25f,-0.05f), new Vector2(3f,1f),CapsuleDirection2D.Horizontal,0, LayerMask);
        return enterCollider;
    }

    public void hit()
    {
        Collider2D enterCollider =  AttackCheck();
        if (enterCollider?.tag == "Ground"||enterCollider?.tag == "Playform")
        {
            rb.velocity = new Vector3(0,0,0);
            animator.SetTrigger("Hit");
            Destroy(gameObject,0.5f);
        }

        else if (enterCollider?.tag == "Player")
        {
            collider2D.enabled = false;
        }

        else if(enterCollider?.tag == "Enemy")
        {
            //attack
            Character beAttacker = enterCollider.GetComponent<Character>();
            beAttacker.TakeDamage(this);

            rb.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("Hit");
            Destroy(gameObject,0.5f);
        }

        else
        {
            collider2D.enabled = true;
            Destroy(gameObject, 10);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position,checkRudius);
    }
}




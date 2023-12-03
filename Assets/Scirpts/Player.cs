using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _Defense;
    public float _Attack;
    public float _Speed;
    public float Score = 0;
    public GameObject _player;
    float facing=1;
    public float hurtForce;
    private PlayerAnimation playerAnimation;
    Rigidbody2D Rb;
    private const int MoveSpeed=3;
    //public int combo;

    [Header("״̬")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isOnGround = false;

    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        playerAnimation=GetComponent<PlayerAnimation>();

    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        if (!isHurt && !isDead)
        {
            Attack();
            Move();
        }
    }

    void Facing()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            facing = 1;
        }
        else if(Input.GetKeyDown(KeyCode.A)) 
        {
            facing = -1;
        }
    }

    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        float Speed = Input.GetAxis("Horizontal") * MoveSpeed;
        if (Input.GetKey(KeyCode.A))
        {
            Facing();
            //Debug.Log(Rb.velocity.x);
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            _player.transform.localScale = new Vector3(facing, 1, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Facing();
            //Debug.Log(Rb.velocity.x);
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            _player.transform.localScale = new Vector3(facing, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.W) && isOnGround == true)
        {
            //Debug.Log(isOnGround);
            Rb.AddForce(new Vector3(0, 1.5f, 0) * 5, ForceMode2D.Impulse);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerAnimation.PlayerAttack();
            isAttack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {;
        if(other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (other.CompareTag("Trap"))
        {
            //Debug.Log(other.tag);
            Destroy(other.gameObject);
            Rb.AddForce(new Vector2(0, 40) * 80);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOnGround=false;
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        Rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x-attacker.position.x),0).normalized;

        Rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
    }
}

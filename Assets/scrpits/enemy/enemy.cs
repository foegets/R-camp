using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float hurttime;
    public Vector2 dir;
    public Transform attacker;
    protected Rigidbody2D rb;
    physicalcheck physicalcheck;
    character character;
    float backcounter;
  protected Animator anim;//只有子类可以访问
    // Start is called before the first frame update
    [Header("基本参数")]
    public float hurtforce;
    public float normalspeed;
    public float chasespeed;
    public float currentspeed;
    public float JumpForceenemy;
    public Vector3 facedir;
    public bool ishurt;
    private void Awake()
    {
        hurttime = 0f;
       character = GetComponent<character>();
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        physicalcheck=GetComponent<physicalcheck>();
    }
    // Update is called once per frame
    
    public virtual void FixedUpdate()
    {
        hurttime -= Time.deltaTime;
        if (transform.localScale.x > 0)
            facedir = new Vector3(-1, 0, 0);
        else
            facedir = new Vector3(1, 0, 0);
        if ((physicalcheck.TouchLeftWall || physicalcheck.TouchRightWall) && backcounter <= 0)
        {
            transform.localScale = new Vector3(9 * facedir.x, 8, 1);
            backcounter = 0.5f;
        }
        backcounter -= Time.deltaTime;
        if (character.currenthealth >= 1 && hurttime <= 0)
            Move();
    }
    public virtual void Move()
    {

        rb.velocity = new Vector2(currentspeed * facedir.x * Time.deltaTime, rb.velocity.y);

    }
    public void Ontakedamage(Transform attackTrans)
    {
        hurttime = 0.3f;
        attacker = attackTrans;
        rb.velocity = Vector2.zero;
        if (transform.position.x - attackTrans.position.x >= 0)
        {
            dir = new Vector2(1.5f, 1.5f).normalized;
            
        }
        else
        {
            dir = new Vector2(1.5f, 1.5f).normalized;
           
        }
        rb.AddForce(dir * hurtforce, ForceMode2D.Impulse);
    }
}

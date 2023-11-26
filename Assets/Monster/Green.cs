using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Green : MonoBehaviour

{   //基本参数
    protected Rigidbody2D rb;
    protected Animator an;
    public float nomalSpeed;
    public float nowSpeed;
    //攻击
    public float damage;
    public float health;
    //巡逻
    public GameObject pa;
    public GameObject pb;
    private Transform currentPoint;
    //死亡
    private bool isLive = true;
    //音效
    AudioSource au;
    public AudioClip Fight;
    public AudioClip Die;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = pb.transform;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        nowSpeed = nomalSpeed;
    }

    void Move()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pb.transform )
        {
            rb.velocity = new Vector2 (nowSpeed, 0);          
        }
        else
        {
            rb.velocity = new Vector2 (-nowSpeed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pb.transform)
        {
            currentPoint = pa.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pa.transform)
        {
            currentPoint = pb.transform;
        }
    }

    private void Derection()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
    void Update()
    {
        Move();
        Lost();
        Derection();
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Lost()
    {
        if (health <= 0)
        {
            an.SetTrigger("Die");         
            isLive = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLive)
        {
            if (collision.gameObject.tag == "Player")
            {
                an.SetTrigger("Atack");
                
                collision.GetComponent<Playermovement>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            an.SetBool("Idle", true);
        }
    }
}

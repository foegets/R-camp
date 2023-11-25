using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private Rigidbody2D rb;

    private Collider2D collider2d;

    public float speed;//平台移动速度

    public float time_1,time_2;//时间记录点1，2

    public float moveTime;//平台总移动时间

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        time_1 = Time.time;


        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3 (speed*Time.fixedDeltaTime,0,0);
        platformLoopMove();
    }

    void platformLoopMove ()
    {
        
        time_2 = Time.time;
        if (time_2 - time_1 >= moveTime)
        {
            speed = -speed;
            time_1 = Time.time;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player"||collision.gameObject.tag=="Weapon")
        {
            Debug.Log("OnTrigger Playform");
            //Rigidbody2D collisionRig = collision.gameObject.GetComponent<Rigidbody2D>();
            //collisionRig.velocity = new Vector2(speed,0);
            collision.transform.position += new Vector3(speed * Time.fixedDeltaTime, 0,0);
            rb.velocity = new Vector3(0,0,0);
        }
    }
}

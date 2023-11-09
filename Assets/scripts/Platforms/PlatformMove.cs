using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;//平台移动速度

    public float time_1,time_2;//时间记录点1，2

    public float moveTime;//平台总移动时间

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        time_1 = Time.time;

        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(speed, 0, 0);
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
}

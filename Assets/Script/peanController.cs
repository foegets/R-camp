using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peanController : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("ÒÆ¶¯²ÎÊý")]
    public float speed = 8f;
    private float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void  Movement()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        turnRound();
    }

    void turnRound()
    {
        if (rb.velocity.x < 0)
            transform.localScale = new Vector2(-1, 1);
        else if (rb.velocity.x > 0)
            transform.localScale = new Vector2(1, 1);
    }
}

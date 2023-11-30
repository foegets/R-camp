using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunMove : MonoBehaviour
{
    private float time;
    public UnityEvent Attack;
    // Start is called before the first frame update
    public Rigidbody2D rb;
    [Header("基本参数")]
    public float speed;

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void FixedUpdate()
    {
        Attack?.Invoke();
    }

    private void move()
    {
        rb.velocity =new Vector2(speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        { Destroy(gameObject); }
    }
}

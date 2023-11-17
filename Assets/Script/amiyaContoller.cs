using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amiyaContoller : MonoBehaviour
{
    private Rigidbody2D rig;
    private BoxCollider2D box;
    private Animator ani;

    [Header("移动参数")]
    public float moveSpeed;

    [Header("跳跃参数")]
    public bool isOnground;
    public float jumpSpeed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        IsOnground();
        Jump();
    }
    
    void IsOnground()
    {
        isOnground = box.IsTouchingLayers(LayerMask.GetMask("platform"));
        Debug.Log("isOnground");
    }
    void Run()
    {
          float speed = Input.GetAxis("Horizontal")*moveSpeed;
          rig.velocity = new Vector2(speed, rig.velocity.y);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump")&&isOnground)
        {
            Vector2 jumpVel = new(0.0f, jumpSpeed);
            rig.velocity = Vector2.up * jumpVel;
        }
    }
}

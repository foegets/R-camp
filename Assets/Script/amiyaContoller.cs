using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amiyaContoller : MonoBehaviour
{
    private Rigidbody2D rig;
    private BoxCollider2D box;

    [Header("移动参数")]
    public float moveSpeed;

    [Header("跳跃参数")]
    public bool isOnground;
    public float jumpSpeed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxis("Horizontal")*moveSpeed;
        rig.velocity = new Vector2(speed, rig.velocity.y);
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

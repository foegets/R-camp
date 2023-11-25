using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jump : MonoBehaviour
{
    [SerializeField] float jumpHeight = 2.5f;
    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;
    private int JumpLimit = 2;
    public LayerMask ground;
    public Rigidbody2D Rb;
    public Rigidbody2D Feet;
    public Rigidbody2D Platform;
    bool mIsOnGround = true;
    bool mIsOnPlatform=true;


    /*bool isGround()
    {
        return Physics.Raycast(rb.position, Vector2.up, 1.5f, ground);
    }*/
    private void CheckIsOnGround()
    {
        mIsOnGround = Feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
    }
    private void CheckIsOnPlatform()
    {
        mIsOnPlatform = Feet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"));
    }
    private void FixedUpdate()
    {
        CheckIsOnGround();
        CheckIsOnPlatform();
        MoveTogether();
    }
    private void Update()
    {
        _Jump();
    }
    void _Jump()
    {
        if (mIsOnGround|| mIsOnPlatform)
        {
            
            JumpLimit = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpLimit > 0)
            {
                Rb.gravityScale = gravityScale;
                float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * Rb.gravityScale) * -2) * Rb.mass;
                Rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                JumpLimit--;
            }
            if (Rb.velocity.y > 0)
            {
                Rb.gravityScale = gravityScale;
            }
            else
            {
                Rb.gravityScale = fallGravityScale;
            }
        }
    }
    private void MoveTogether()
    {
        if (mIsOnPlatform)
        {
            Rb.transform.parent = Platform.transform;
            Debug.Log(1);
        }
        if (!mIsOnGround)
        {
            Rb.transform.parent = null;
        }
    }
}

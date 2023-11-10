using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] float jumpHeight = 2.5f;
    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;
    private int JumpLimit = 2;
    public Rigidbody2D rb;
    public LayerMask ground;
    public Transform feet;
    bool mIsOnGround = true;
  
    
    /*bool isGround()
    {
        return Physics.Raycast(rb.position, Vector2.up, 1.5f, ground);
    }*/
    private void CheckIsOnGround()
    {
        mIsOnGround = rb.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    private void Update()
    {
        CheckIsOnGround();
        _Jump();
        
    }
    void _Jump()
    {
        if (mIsOnGround)
        {
            Debug.Log(1);
            JumpLimit = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpLimit > 0)
            {
                rb.gravityScale = gravityScale;
                float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                JumpLimit--;
            }
            if (rb.velocity.y > 0)
            {
                rb.gravityScale = gravityScale;
            }
            else
            {
                rb.gravityScale = fallGravityScale;
            }
        }
    }
    //改后问题，落地后JumpLimit不会回复到2

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float MoveSpeed = 10.0f;
    public Rigidbody2D rb;
    public float xVelocity;

    // Update is called once per frame

    private void FixedUpdate()
    {
        GroundMovement();
        FilpDirction();
    }
    void GroundMovement()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * MoveSpeed, rb.velocity.y);
    }
    void FilpDirction()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector2(-5, 5);
        if (xVelocity > 0)
            transform.localScale = new Vector2(5, 5);
    }
}

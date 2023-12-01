using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float MoveSpeed = 10f;
    public Rigidbody2D rb;
    public float xVelocity;
    public float hurtForce;
    public bool isHurt=false;


    // Update is called once per frame

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            GroundMovement();
            FilpDirction();
        }
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
    public void GetHurt(Transform attacker)
    {   
        isHurt = true;
        rb.velocity=Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x-attacker.position.x),0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        Debug.Log(1);
    }
}

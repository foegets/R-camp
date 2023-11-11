using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform1Move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movingSpeed;
    public int direction = 1;
    private void FixedUpdate()
    {
        float positiony = transform.position.y;
       
        if (positiony < -2 )
        {
            direction = 1;
        }
        if (positiony > 5)
        {
            direction = -1;
        }
        rb.velocity = new Vector2( rb.velocity.x, direction * movingSpeed * Time.fixedDeltaTime);
    }
}

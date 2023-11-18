using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2Move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movingSpeed;
    public int direction = 1;
    
    private void FixedUpdate()
    {
        float positionx = transform.position.x;

        if (positionx < 7)
        {
            direction = 1;
        }
        if (positionx > 14.5)
        {
            direction = -1;
        }
        rb.velocity = new Vector2(direction * movingSpeed * Time.fixedDeltaTime,rb.velocity.y);
    }
}

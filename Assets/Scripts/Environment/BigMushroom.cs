using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMushroom : MonoBehaviour
{
    public float upSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hello");
        //rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y+100);
       collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, upSpeed);
    }
}

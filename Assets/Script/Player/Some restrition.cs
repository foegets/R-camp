using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Some : MonoBehaviour
{
    [SerializeField] float jumpForce = 1000000;
    public Rigidbody2D body;
    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
            body.AddForce(Vector2.left * jumpForce);
            Debug.Log(233);
        }//要做到碰到collision之后彻底改变运动性质
    }
}

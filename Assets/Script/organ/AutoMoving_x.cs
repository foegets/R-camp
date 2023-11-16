using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoMoving : MonoBehaviour
{
    [SerializeField] float Speed = 5;
    public Rigidbody2D rb;
    public Collider2D collision;
    // Start is called before the first frame update

    private void  FixedUpdate()
    {
       
        AutoMove();
        
    }
    private void AutoMove()
    {
       
        Vector2 pos = transform.position;
        pos.y = -2.36f;
        if (rb.position.y!=-2.13)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "back")
        {
            Speed = -Speed;
            Debug.Log(233);
        }//要做到碰到collision之后彻底改变运动性质
    }
}

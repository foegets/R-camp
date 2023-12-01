using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_x : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float Speed = 5;
    public Animator anim;
    public Rigidbody2D rb;
    public Collider2D collision;
    public bool Death = true;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        AutoMove();
    }
    private void AutoMove()
    {

        Vector2 pos = transform.position;
        pos.y = -2.36f;
        if (rb.position.y != -2.13)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }

    }

    // Start is called before the first frame update


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "back")
        {
            Speed = -Speed;
            transform.localScale = new Vector2(Speed, 5);
        }//要做到碰到collision之后彻底改变运动性质
        if (collision.tag == "Arrow")
        {
            health--;
            Destroy(collision.gameObject);
            if (health == 0)
            {
                anim.SetBool("Dead", Death);
                Speed = 0;
            }

        }
    }
}

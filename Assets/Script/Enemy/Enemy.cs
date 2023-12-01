using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed = 5;
    [SerializeField] int health;
    public Animator anim;
    public PlayableDirector director;
    public Rigidbody2D rb;
    public Collider2D collision;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        AutoMove();
    }
    private void AutoMove()
    {
        Vector2 pos = transform.position;
        pos.x = -13;
        transform.Translate(0, Speed * Time.deltaTime, 0);

    }
        void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "back")
        {
            Speed = -Speed;
        }//要做到碰到collision之后彻底改变运动性质
        if (collision.tag == "Arrow")
        {
            health--;
            Destroy(collision.gameObject);
            if (health == 0)
            {
                director.Play();
                Speed = 0;
                Destroy(anim.gameObject,1.2f);
            }
        }
       
    }
}

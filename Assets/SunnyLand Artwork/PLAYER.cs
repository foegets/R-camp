using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float jumpforce;
    private bool isground;
    public Transform gameobject;
    public LayerMask whatisground;
    private Animator anim;
    private SpriteRenderer sr;
    public float knockback, knockbackforce;
    // Start is called before the first frame update
    void Start()
    {
        anim =GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        isground = Physics2D.OverlapCircle(gameobject.position, .2f,whatisground);

        rb.velocity = new Vector2(speed*Input.GetAxis("Horizontal"),rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            if (isground)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);

            }
        }
        anim.SetFloat("speed", Mathf.   Abs(rb.velocity.x));
        anim.SetBool("isground", isground);
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }
}

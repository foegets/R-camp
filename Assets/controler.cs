using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class controler : MonoBehaviour
{
    public int speed = 2;
    public Rigidbody2D rb;
    private Vector2 position;
    private Vector2 move;
    public int jumpforce=8;
    public LayerMask groud;
    
    public float check_static =5.0f;
    public bool is_groud=false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        //float Vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(Horizontal  * speed, rb.velocity.y);
        position = position + move * Time.fixedDeltaTime;
        check();
        if (Input.GetKeyDown(KeyCode.Space)&&is_groud)
        {
            Debug.Log("jump");
            jump();
        }
        
    }
    private void FixedUpdate( )
    {
        
        //rb.velocity = move;
        rb.MovePosition(position);
        
    }
    void jump() {
        //Vector2 heiht = new (0,1);
        rb.AddForce(transform.up*jumpforce,ForceMode2D.Impulse);
    
    }
    void check() {
        
        is_groud = Physics2D.OverlapCircle(
            transform.position, 0.1f, groud);
        Debug.Log(is_groud);
    
    
    }
}

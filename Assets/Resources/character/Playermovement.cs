using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovement : MonoBehaviour
{
    //ÐÐ×ß
    private Rigidbody2D rb;
    public Animator An;
   

    public float speed = 8f;
    private float xVelocity;
    //ÌøÔ¾
    public bool isGrounded ;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int n;
    public float force = 15.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        An  = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            n=2;
            
        }
        if (Input.GetKeyDown(KeyCode.Space)&&n>1)
        {
            rb.velocity = Vector2.up*force;
            
            An.SetBool("Jump", true);
            n--;
        }

        SwitchAnimation();
    }

    private void FixedUpdate()
    {
        GroundMovement();
        Direction();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

    }


    void GroundMovement()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        
    }

    void Direction()
    {
        if (xVelocity < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (xVelocity > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag=="coin")
        {
            Coincounter.nowCoin += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            An.SetBool("Die", true);
            
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SwitchAnimation()
    {
        An.SetBool("Ide", false);
        if (An.GetBool("Jump"))
        {
            if (rb.velocity.y < 0.0f)
            {
                An.SetBool("Jump", false);
                An.SetBool("Fall", true);
            }
        }
        else if (isGrounded==true)
        {
            An.SetBool("Ide", true);
            An.SetBool("Fall", false);
        }
    }

    
    
}
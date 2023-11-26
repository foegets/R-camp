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
    int n;
    public float force = 15.0f;
    //¹¥»÷
    public float damage;
    //ÑªÁ¿
    public float health;
    //ÒôÐ§
    AudioSource Au;
    public AudioClip Jumpping;
    public AudioClip Coin;
    public AudioClip Die;
    public AudioClip Fight;
    //ÍÏÎ²
    public GameObject Trail;
    //ËÀÍö
    bool isLive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        An  = GetComponent<Animator>();
        Au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive) 
        {
            GroundMovement();
            Direction();
            Atack(); 
            Jump(); 
        }
        if (health <= 0)
        {
            Gameover();
        }

        SwitchAnimation();
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

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            n = 2;
        }
        if (Input.GetKeyDown(KeyCode.Space) && n > 1)
        {
            rb.velocity = Vector2.up * force;
            Au.clip = Jumpping;
            Au.Play();
            An.SetBool("Jump", true);
            n--;
        }
    }

    void Atack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            An.SetTrigger("Atack");
            Au.clip = Fight;
            Au.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "coin")
        {
            Au.clip = Coin;
            Au.Play();
            Coincounter.nowCoin += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag=="Trap")
        {
            Gameover();
        }
        else if (collision.gameObject.tag == "Enermy")
        {
            collision.GetComponent<Green>().TakeDamage(damage);
        }
    }

    void Gameover()
    {
        if (isLive)
        {
           Au.clip = Die;
           Au.Play();
           Trail.SetActive(false);
           An.SetBool("Die", true);
           isLive = false;
        }
       
    }
    public void Restart()
    {        
        SceneManager.LoadScene("Play");
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
        else if(rb.velocity.y < 0.0f)
        {
            An.SetBool("Fall", true);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }




}
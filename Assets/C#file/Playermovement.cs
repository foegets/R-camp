using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Playermovement : MonoBehaviour
{
    //����
    private Rigidbody2D rb;
    public Animator An;
    public float speed = 8f;
    private float xVelocity;
    //��Ծ
    public bool isGrounded ;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    int n;
    public float force = 15.0f;
    //����
    public int damage;
    //Ѫ��
    public int health;
    //��Ч
    AudioSource Au;
    public AudioClip Jumpping;
    public AudioClip Coin;
    public AudioClip Die;
    public AudioClip Fight;
    //��β
    public GameObject Trail;
    //����
    bool isLive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        An  = GetComponent<Animator>();
        Au = GetComponent<AudioSource>();
        Healthbar.maxHealth = health;
        Healthbar.currentHealth = Healthbar.maxHealth;
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
            Healthbar.currentHealth = 0;
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
    public void TakeDamage(int damage)
    {
        health -= damage;
        An.SetTrigger("Hurt");
        Healthbar.currentHealth = health;
        if (health <= 0)
        {
            Healthbar.currentHealth = 0;
        }
    }




}
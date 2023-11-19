using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovement : MonoBehaviour
{
    //––◊ﬂ
    private Rigidbody2D rb;
    public Animator An;
    public float speed = 8f;
    private float xVelocity;
    //Ã¯‘æ
    public bool isGrounded ;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    int n;
    public float force = 15.0f;
    //“Ù–ß
    AudioSource Au;
    public AudioClip Jumpping;
    public AudioClip Coin;
    public AudioClip Die;
    public AudioClip Fight;
    //ÕœŒ≤
    public GameObject Trail;
    //À¿Õˆ
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
            An.SetBool("Atack", true);
            Au.clip = Fight;
            Au.Play();
        }
    }

    void AtackExit()
    {
        An.SetBool("Atack", false);
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
            isLive = false;
            Au.clip = Die;
            Au.Play();
            Trail.SetActive(false);
            An.SetBool("Die", true);
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
    }

    
    
}
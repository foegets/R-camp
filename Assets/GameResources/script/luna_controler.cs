using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class luna_controler : MonoBehaviour
{   
    int jumpforce = 12;
    //public float G_force;
    public int speed = 5;
    public bool is_groud=false;
    private LayerMask groud;
    public int point=0;
    public TextMeshProUGUI text;
    public attack_control attack_Control;
    

    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        groud = LayerMask.GetMask("groud");  //获取layer  
        animator = GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale =1;
        

    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"point:{point}";

        float Horizontal = Input.GetAxis("Horizontal");
        //animator.SetFloat("Horizontal", Horizontal);
        looking_at(Horizontal);
        
        attack(); 
        rb.velocity = new Vector2(Horizontal*speed, rb.velocity.y);
        check();
        if (Input.GetKeyDown(KeyCode.Space)&&is_groud) {
            //Debug.Log("jump");
            jump();
        
        }
    }
    void jump() {
        rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    
    }
    private void check()
    {
        is_groud = false;
        is_groud = Physics2D.OverlapCircle(
            (Vector2)transform.position + new Vector2(0f, -1f), 0.2f, groud);
        
    }
    
    public void add_point()
    {
        point++; 
    }public void attack()
    {
        if (attack_Control.handing)
        {
            speed = 2;
        }
        else
        {
            speed = 5;
        }
        if (attack_Control.attacking) { speed = 0; }
    }
    
    private void looking_at(float horizontal)
    {
        if (horizontal > 0.1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // 将角色绕Y轴旋转180度
            animator.SetBool("is_walking", true);
        }
        else if (horizontal < -0.1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // 将角色绕Y轴旋转180度
            animator.SetBool("is_walking", true);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0); // 将角色绕Y轴旋转180度
            animator.SetBool("is_walking", false);
        }
    }
}

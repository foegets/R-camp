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
    int speed = 5;
    public bool is_groud=false;
    private LayerMask groud;
    public int point=0;
    public TextMeshProUGUI text;

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
        animator.SetFloat("Horizontal", Horizontal);
        //Debug.Log(Horizontal);
        rb.velocity = new Vector2(Horizontal*speed, rb.velocity.y);
        //Debug.Log("1");
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
        //gravity();
    }
    //private void gravity()
    //{
    //    if (!is_groud) { rb.AddForce(-transform.up * G_force * Time.fixedDeltaTime, ForceMode2D.Impulse); }
    //    //if (!is_groud) { rb.AddForce(-transform.up * G_force, ForceMode2D.Impulse); }  写Vector2.down来表示绝对向下也可以
    //}
    public void add_point()
    {
        point++; 
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+new Vector2(0f,-1f), 0.2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

 protected Rigidbody2D rb;
    physicalcheck physicalcheck;
    float backcounter;
  protected Animator anim;//只有子类可以访问
    // Start is called before the first frame update
    [Header("基本参数")]
    public float normalspeed;
    public float chasespeed;
    public float currentspeed;
    public float JumpForceenemy;
    public Vector3 facedir;
    private void Awake()
    {
       
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        physicalcheck=GetComponent<physicalcheck>();
    }
    // Update is called once per frame
    
    public virtual void FixedUpdate()
    {
        if (transform.localScale.x > 0)
            facedir = new Vector3(-1, 0, 0);
        else
            facedir = new Vector3(1, 0, 0);
        if ((physicalcheck.TouchLeftWall || physicalcheck.TouchRightWall) && backcounter <= 0)
        {
            transform.localScale = new Vector3(9 * facedir.x, 8, 1);
            backcounter = 0.5f;
        }
        backcounter -= Time.deltaTime;
     
        Move();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentspeed * facedir.x * Time.deltaTime, rb.velocity.y);

    }
}

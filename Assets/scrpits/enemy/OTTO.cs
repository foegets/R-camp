using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTTO : enemy
{
    
    float jumpcounter = 5f; 
    private void Update()
    {
        jumpcounter-=Time.deltaTime;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (currentspeed <= 1500)
            currentspeed += 5*Time.deltaTime;
    }
    public override void Move()
    {
        base.Move();//保留父类方式
        anim.SetBool("ismove", true);
        if(jumpcounter<0)
        {
            rb.AddForce(transform.up * JumpForceenemy, ForceMode2D.Impulse);
            jumpcounter = 5f;
        }
    }
}

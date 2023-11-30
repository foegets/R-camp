using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OTTO : enemy
{
    private float limit;
    public UnityEvent Finish;
    float jumpcounter = 5f;
    character character1;
    public override void Awake()
    {
        base.Awake();
        character1 = GetComponent<character>();
        limit = 1;
    }
    private void Update()
    {
        jumpcounter-=Time.deltaTime;
     
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (currentspeed <= 1500)
            currentspeed += 5*Time.deltaTime;
        if (character1.currenthealth <= 0 && limit == 1)
        {
            Finish?.Invoke();
            limit = 0;
        }

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

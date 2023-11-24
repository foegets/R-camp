using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seaMonster : Enemy
{
    public float startWaitTime;
    public float waitTime;
    public float speed;

    public bool isFind;

    public Transform movePos;
    public Transform leftPos;
    public Transform rightPos;
    public Animator ani;
    private Rigidbody2D rig;

    void Start()
    {
        Debug.Log("1");
        base.Start();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        movePos.position= new Vector2(Random.Range(leftPos.position.x, rightPos.position.x), transform.position.y);
    }

    void Update()
    { 
        base.Update();
        pursuit();
        Run();
    }

   void pursuit()
   {
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        Filp();
        if(!isFind)
        {  
            Debug.Log("pursuit");
            if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
            {
                if(waitTime<0.1f)
                {
                    movePos.position = new Vector2(Random.Range(leftPos.position.x, rightPos.position.x), transform.position.y);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
         
        
   }


    void Run()
    {
        if(transform.position!=movePos.position)
        {
            ani.SetBool("isRun", true);
        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }

    void Filp()
    {
         if (transform.position.x<movePos.position.x)
         {
             transform.localRotation = Quaternion.Euler(0, 0, 0);
         }
         else
         {
             transform.localRotation = Quaternion.Euler(0, 180, 0);
         }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("onttigger");
            isFind = true;
            movePos.position = new Vector2(other.transform.position.x,transform.position.y);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFind = false;
        }
    }
}

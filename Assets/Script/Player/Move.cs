using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update

    public float MoveSpeed = 10.0f;
    public Rigidbody2D rb;
    public float xVelocity;
    int i = 1;
    int j = 1;
    // Update is called once per frame

    private void FixedUpdate()
    {   if(i==1)
        GroundMovement();
        else if (i == -1)
        GroundRush();
        if(j==-1)
        GroundWalk();
        FilpDirction();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            i = -i;//切换静步与疾走 -1为静步 1为疾走
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))

        {
            j = -j;//切换疾跑与冲刺 -1为冲刺 1为疾走
        }
    }
    void GroundMovement()
    {
         xVelocity = Input.GetAxis("Horizontal");
        rb.velocity=new Vector2(xVelocity*MoveSpeed, rb.velocity.y);
    }
    void GroundRush()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * MoveSpeed*2, rb.velocity.y);
    }
    void GroundWalk()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * MoveSpeed/2, rb.velocity.y);
    }
    void FilpDirction()
    {
        if (xVelocity < 0)
            transform.localScale = new Vector2(-5, 5);
        if(xVelocity>0)
            transform.localScale = new Vector2(5, 5);
    }


}

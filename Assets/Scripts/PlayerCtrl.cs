using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    public Rigidbody2D rb;
    [SerializeField] Vector3 feetPos;
    [SerializeField] float movement_spd = 5f;
    [SerializeField] float jump_spd = 9f;

    int facing = 1;



    void Awake()
    {
        inputHandler.DoJump += DoJump;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    Vector2 veloctiy = Vector2.zero;
    void Movement()
    {
        float x = inputHandler.MovementInput.x;

        //同步y轴速度
        veloctiy.y = rb.velocity.y;
        
        veloctiy.x = x * movement_spd;

        rb.velocity = veloctiy;
    }

    void Facing()
    {
        if(veloctiy.x > 0.1f && facing < 0)
        {
            facing = 1;
        }
        else if(veloctiy.x < -0.1f && facing > 0)
        {
            facing = -1;
        }


    }

    int jump_count = 0;
    void DoJump()
    {
        
        if (IsOnGround())
        {
            jump_count = 0;
        }

        if(jump_count < 2)
        {
            veloctiy.y = jump_spd;
            rb.velocity = veloctiy;
            print("Jump");
            jump_count++;
        }
    }

    RaycastHit2D[] rch;
    Vector2 size = new Vector2(0.6f, 0.2f);
    bool IsOnGround()
    {
        rch = Physics2D.BoxCastAll(transform.position + feetPos, size/2, 0, Vector2.down, 0.3f, LayerMask.GetMask("Level", "Player")); 
        return rch.Length > 0;
    }



    

}

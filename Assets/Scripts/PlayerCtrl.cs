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

    void DoJump()
    {
        if (IsOnGround())
        {
            veloctiy.y = jump_spd;
            rb.velocity = veloctiy;
            print("Jump");
        }
    }

    RaycastHit2D[] rch;
    Vector2 size = new Vector2(0.6f, 0.2f);
    bool IsOnGround()
    {
        rch = Physics2D.BoxCastAll(transform.position + feetPos, size/2, 0, Vector2.down, 0.3f, LayerMask.GetMask("Level"));
        return rch.Length > 0;
    }



    

}

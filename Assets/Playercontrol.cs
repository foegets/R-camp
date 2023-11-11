using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class Playercontrol : MonoBehaviour
{
    // Start is called before the first frame update
    int limit;
    public PlayerInputControl inputcontrol;
    private Rigidbody2D rb;
    public Vector2 inputdirection;
    private physicalcheck physicalcheck1;
    private physicalcheck rebirth1;
    [Header("基本参数")]
    public float Speed,Fast=0;
    public float JumpForce;


    // Update is called once per frame
    private void Awake()
    {
        physicalcheck1=GetComponent<physicalcheck>();//获得文件内的公开变量
        rb = GetComponent<Rigidbody2D>();
        inputcontrol=new PlayerInputControl();
        inputcontrol.GamePlay.Jump.performed += Jump;
        inputcontrol.GamePlay.spacial.performed += Spacial;
        
    }


    private void OnEnable()
    {
        inputcontrol.Enable();
    }
    private void OnDisable()
    {
        inputcontrol.Disable();
    }
    private void Update()
    {
        inputdirection = inputcontrol.GamePlay.Move.ReadValue<Vector2>();
        returnlocation();
    }
    private void FixedUpdate()
    {
        returnlocation();
        Move();
      
    }

    private void returnlocation()
    {
        if (rb.velocity.y <= -100)
            rb.velocity = new Vector2(0,(float)0.0664);
    }

    public void Move()
    {
        if(Fast==0)
        rb.velocity = new Vector2(inputdirection.x * Speed * Time.deltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(inputdirection.x * Speed * Time.deltaTime*2, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;
        if (inputdirection.x > 0)
            faceDir = 2;
        if (inputdirection.x < 0)
            faceDir = -2;
        transform.localScale = new Vector3(faceDir, 2, 2);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (physicalcheck1.isGround)
             limit = 1;
        if (physicalcheck1.isGround || limit >= 0)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            limit--;
        }
        Debug.Log("jump");
    }
    private void Spacial(InputAction.CallbackContext context)
    {
        if (Fast == 0)
        { Fast = 1;
            Debug.Log("1");
         }
        else
        {
            Fast = 0;
            Debug.Log("0");
        }
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaterController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Vector2 inputDirection;
    public float speed;
    private Rigidbody2D rb;
    private PhysicCheck physicCheck;
    public float jumpForce;
    public float boomForce;

    public void Move()//移动函数
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);//移动

        int dir = (int)transform.localScale.x;//临时变量，当inputDirection不等于1或-1时将其转化为1或-1
        if(inputDirection.x > 0) dir = 1;
        if (inputDirection.x < 0) dir = -1;

        transform.localScale = new Vector3(dir,1,1);  //人物反转

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//实例化rb
        physicCheck = GetComponent<PhysicCheck>();

        inputControl = new PlayerInputControl();

        inputControl.GamePlay.Jump.started += Jump; 

    }

   

    private void OnEnable()
    {
        inputControl.Enable();//初始时激活input获取输入
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnDisable()
    {
        inputControl.Disable();//销毁input
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();//每帧从输入中读取朝向用于Move（）

        if (physicCheck.isboom)
            rb.AddForce(transform.up * boomForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("jump"); 
        if(physicCheck.isGround)
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
    }

}
 
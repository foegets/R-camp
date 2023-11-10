using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputControl;
    private Rigidbody2D rb;
    public Vector2 inputDirection;
    public float speed;//
    public float jumpForce;
    private PhysicsCheck physicsCheck;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  //获取object刚体数据
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputController();
        inputControl.Player.Jump.started += Jump;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        inputDirection=inputControl.Player.Move.ReadValue<Vector2>();  //储存键盘操作后值
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        int faceDirection = (int)transform.localScale.x;  //储存初始比列x
        if (inputDirection.x > 0)
            faceDirection = 1;
        if (inputDirection.x < 0)
            faceDirection = -1;  //根据键盘输入判断是否转向
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y); 
        //Time.deltaTime为增量时间，根据当前帧数改变，y轴速度维持不变
        transform.localScale = new Vector3(faceDirection, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if(physicsCheck.isGround==true)
        rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
        //Inpulse用于施加瞬时力
    }
}

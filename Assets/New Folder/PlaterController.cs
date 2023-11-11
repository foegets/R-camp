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

    public void Move()//�ƶ�����
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);//�ƶ�

        int dir = (int)transform.localScale.x;//��ʱ��������inputDirection������1��-1ʱ����ת��Ϊ1��-1
        if(inputDirection.x > 0) dir = 1;
        if (inputDirection.x < 0) dir = -1;

        transform.localScale = new Vector3(dir,1,1);  //���ﷴת

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//ʵ����rb
        physicCheck = GetComponent<PhysicCheck>();

        inputControl = new PlayerInputControl();

        inputControl.GamePlay.Jump.started += Jump; 

    }

   

    private void OnEnable()
    {
        inputControl.Enable();//��ʼʱ����input��ȡ����
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnDisable()
    {
        inputControl.Disable();//����input
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();//ÿ֡�������ж�ȡ��������Move����

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
 
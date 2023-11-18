using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public PlayerInputControl inputControl;//С�շ�
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    
    public float speed; //���������С��
    public float jumpForce;

    private void Awake()//privateû�а취��unity�п���������public���ԣ�
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new PlayerInputControl();
        inputControl.GamePlay.Jump.started += Jump;//ע�ắ�����Ѻ���������Ӱ��°�����һ��ִ��
    }
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    //private void ontriggerstay2d(collider2d collision)
    //{
        
    //}
    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);//�����ʹ�� transform �� Position ���Ӽ�ȥ�ƶ� ����Ҫ ���� Time.deltaTime
        int faceDir =(int) transform.localScale.x;                                         //ǿ�ư�float ת��Ϊint                                                                 
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir,1,1);//���﷭ת
    }
      private void Jump(InputAction.CallbackContext context)
    {
        //
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
          Debug.Log("jump");
       }
        }

    }

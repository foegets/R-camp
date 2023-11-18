using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public PlayerInputControll inputControll;
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;  
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public bool isHurt;
    public float hurtForce;
    private void Awake()
    {
        inputControll = new PlayerInputControll();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControll.Player.Jump.started += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        inputControll.Enable();
    }
    private void OnDisable()
    {
        inputControll.Disable();
    }
   
    // Start is called before the first frame update
    // Update is called once per frame
    private void Update()
    {
        inputDirection = inputControll.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        if (!isHurt)
        Move();
    }
    //miao
    private void OnTriggerStay2D(Collider2D other)     
    {
       // Debug.Log(other.name);
    }
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //判断朝向
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);

       

    }
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
}

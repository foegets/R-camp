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
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    private void Awake()
    {
        inputControll = new PlayerInputControll();
        inputControll.Player.Jump.started += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
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
        Move();
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
}

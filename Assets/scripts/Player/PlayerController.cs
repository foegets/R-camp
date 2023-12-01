using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    public Vector2 inputDirection;
    public CapsuleCollider2D coll;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float hurtForce;

   

    private physicsCheck physicscheck;
    private PlayerAnimation playerAnimation;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    

    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();
        physicscheck = GetComponent<physicsCheck>();
        inputControl.GamePlayer.Jump.started += Jump;
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        //攻击
        inputControl.GamePlayer.Attack.started += PlayerAttack;
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
        inputDirection = inputControl.GamePlayer.Move.ReadValue<Vector2>();
        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        
            Move();
        
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);

        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        //人物翻转 
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

 
    private void Jump(InputAction.CallbackContext obj)
    {
        if(physicscheck.isGround)
        //Debug.Log("Jump");
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
        
    }

    #region UnityEvent
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir*hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlayer.Disable();
        
    }
    #endregion

    private void CheckState()
    {
        coll.sharedMaterial = physicscheck.isGround ? normal : wall;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Vector2 inputDirection;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playAnimation;
    [Header("基本参数")]
    public float speed;//左右移动
    public float jumpForce;//跳跃
    public float injureForce;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;


    [Header("状态")]
    public bool isInjure;
    public bool isDie;
    public bool isAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        playAnimation = GetComponent<PlayerAnimation>();

        inputControl = new PlayerInputControl();

        //跳跃
        inputControl.Gameplay.Jump.started += Jump;

        //攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;
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
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()//跟物理有关的都在这执行
    {   
        if(!isInjure && !isAttack)
            Move();   
    }

    public void Move()
    {
        //左右移动
        rb.velocity = new Vector2(inputDirection.x * speed /* Time.deltaTime*/, rb.velocity.y);

        double faceDir = (int)transform.localScale.x;//(这个代码会使人物在开局的时候消失不见，不知道怎么解决）

        if (inputDirection.x > 0)
            faceDir = 1;
        if(inputDirection.x < 0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3((float)faceDir, 1, 1);
    }


    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        if(physicsCheck.isGround)
        rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        if (!physicsCheck.isGround)
            return;
        //Debug.Log("attack");
        playAnimation.PlayerAttack();
        isAttack = true;
    }


    public void GetInjure(Transform attacker)
    {
        isInjure = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir * injureForce,ForceMode2D.Impulse);
    }

    public void PlayerDie()
    {
        isDie = true;
        inputControl.Gameplay.Disable();
        Invoke("GameoverMenu", 1f);
    }

    private void GameoverMenu()
    {
        SceneManager.LoadScene(2);
    }

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}

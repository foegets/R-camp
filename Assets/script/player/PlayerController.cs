using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameInput inputControl;
    public Vector2 inputDirection;
    public Rigidbody2D rb;
    //创建变量并在Awake中通过GetComponent获得判断跳跃与地面碰撞的组件
    private PhysicsCheck physicsCheck;
    [Header("基参")]
    public float speed;
    public float jumpForce;
    public float hurtForce;//创建用来施加受伤后反弹的力
    public bool isHurt;//判断受伤
    public bool isDead;//判断死亡
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new GameInput();
        //通过施加力来实现跳跃（注意started，performed，canceled区别）
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
    private void Update()
    {
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        //判断是否受伤来执行move，由于如果isHurt为ture且不变，则受伤后会一直不动，因此在Animator中的blue hurt添加了一个代码，用来结束动作后将isHurt改为false
        if(!isHurt)
            Move();
    }

    public void Move()
    {
        //实现移动
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //通过scale实现人物翻转
        int faceDir = (int)transform.localScale.x;//记录真实方向
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);//通过判断实现翻转
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        //引用跳跃判断组件限制跳跃
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //实现受伤反弹，传入攻击者的参数
    public void GetHurt(Transform attacker)
    {
        isHurt = true;//用于Fixupdate中停止执行move
        rb.velocity = Vector2.zero;
        //通过两者坐标正负判断击退反向,normalize即对x轴坐标计算结果仅保留正负数值归一
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        //添加力，方向乘以力，力的类型即为2D且瞬时
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

    }

    public void PlayerDead()
    {
        isDead = true;
        //死了直接锁键盘实现无法进行移动,我真是天才
        inputControl.Player.Disable();
    }


}   

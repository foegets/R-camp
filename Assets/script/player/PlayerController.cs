using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;//调用大库中InputSystem在下面启用我们创建的GameInput脚本

public class PlayerController : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEventSO loadEvent;//menu中限制人物移动
    public VoidEventSO afterSceneLoadedEvent;//menu切换后启动人物移动
    public GameInput inputControl;
    public Vector2 inputDirection;
    public Rigidbody2D rb;
    public PlayerAnimation playerAnimation;
    public Collider2D coll;
    //创建变量并在Awake中通过GetComponent获得判断跳跃与地面碰撞的组件
    private PhysicsCheck physicsCheck;
    [Header("基参")]
    public float speed;
    public float jumpForce;
    public float hurtForce;//创建用来施加受伤后反弹的力
    public int numberOfJump;//设置连续跳跃次数
    private int currentJumpNumber;
    [Header("状态")]
    public bool isHurt;//判断受伤
    public bool isDead;//判断死亡
    public bool isAttack;//判断攻击
    [Header("材质")]
    public PhysicsMaterial2D normal;//带摩擦的普通材质
    public PhysicsMaterial2D wall;//光滑的用于墙体碰撞的材质
    
    
    //每一个类要启用脚本变量都需要new一个
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        inputControl = new GameInput();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<Collider2D>();
        //读取键盘输入跳跃键来实现跳跃（注意started，performed，canceled区别）
        inputControl.Player.Jump.started += Jump;
        //读取键盘输入攻击键
        inputControl.Player.Attack.started += PlayerAttack;

    }


    //启动PlayerController的时候也启用inputControl
    private void OnEnable()
    {
        inputControl.Enable();
        loadEvent.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        inputControl.Disable();
        loadEvent.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }

    private void Update()
    {
        //时刻更新人物朝向
        inputDirection = inputControl.Player.Move.ReadValue<Vector2>();
        CheckMaterial();
        ResetNumberOfJump();//重置跳跃段数
    }
    private void FixedUpdate()
    {
        //判断是否受伤和攻击来执行move，在Animator中的blue hurt添加了一个代码，用来结束动作后将isHurt改为false，isAttack改为false
        if(!isHurt && !isAttack)
            Move();       
    }

    private void ResetNumberOfJump()
    {
        if (physicsCheck.isGround)
            currentJumpNumber = numberOfJump;
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Player.Disable();
    }
    private void OnAfterSceneLoadedEvent()
    {
        inputControl.Player.Enable();
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

    //实现跳跃
    private void Jump(InputAction.CallbackContext obj)
    {
        //引用跳跃判断组件限制跳跃
        if (currentJumpNumber > 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);//重置y轴速度避免跳跃力叠加
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            currentJumpNumber--;
        }

    }

    //实现攻击
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        //启动PlayerAnimation中的PlayerAttack函数
        playerAnimation.PlayerAttack();
        isAttack = true;

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

    
    public void CheckMaterial()
    {
        //通过人物是否战立更换材质实现在地面有摩擦（使人物攻击时不会滑动），贴墙时无摩擦，三元运算符？：，记得把函数放进update
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

    public void PlayerDead()
    {
        isDead = true;
        //死了直接锁键盘实现无法进行移动
        inputControl.Player.Disable();
    }


}   

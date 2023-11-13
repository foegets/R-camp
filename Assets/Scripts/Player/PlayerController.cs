using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//使用输入系统
using UnityEngine.InputSystem;

//新建子类【玩家操控器】
public class PlayerController : MonoBehaviour
{
    //创建使用输入控制类型的变量
    public PlayerInputControl inputControl;
    //创建刚体rb
    private Rigidbody2D rb;
    //创建物理检测类新变量：physicscheck
    private PhysicsCheck physicscheck;
    //创建二维向量表输入方向
    public Vector2 inputDirection;
    [Header("基本参数")]
    //创建速度（浮点类型）
    public float speed;
    //创建跳跃的力
    public float jumpForce;

    //awake是在一开始要做的部分
    private void Awake(){
        //将玩家的刚体组件赋给rb
        rb = GetComponent<Rigidbody2D>();
        //将玩家的物理检测组件赋给physicscheck
        physicscheck = GetComponent<PhysicsCheck>();
        //实例化一个inputcontrol
        inputControl = new PlayerInputControl();
        //注册一个函数叫jump
        inputControl.Gameplay.Jump.started += Jump;
    }

    //启动
    private void OnEnable()
    {
        inputControl.Enable();
    }

    //关闭
    private void OnDisable(){
        inputControl.Disable();
    }

    //根据实际情况更新
    private void Update(){
        //读入方向所对应的数值value
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    //固定频率更新
    private void FixedUpdate(){
        //进行移动
        Move();
    }

    //创建move函数方法
    public void Move(){
        //实例化一个二维向量，并给到刚体身上
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        
        //解决朝向（翻转）问题
        int faceDir = (int)transform.localScale.x;

        if(inputDirection.x >0)
            faceDir = 1;
        if(inputDirection.x <0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3(faceDir,1,1);
    }

    //创建jump函数方法
    private void Jump(InputAction.CallbackContext obj){
        //Debug.Log("JUMP");
        //给刚体rb施加一个（瞬时的）力，方向向上
        if(physicscheck.isGround)
            rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }
}

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
    //获得一下玩家动画的组件
    private PlayerAnimation playeranimation;
    //获得一下玩家胶囊碰撞体的组件
    private CapsuleCollider2D coll;

    [Header("基本参数")]

    //创建速度（浮点类型）
    public float speed;
    //创建跳跃的力
    public float jumpForce;
    //创建受击时的力
    public float hurtForce;

    [Header("物理材质")]
    //创建没有跳起来的时候的材质
    public PhysicsMaterial2D normal;
    //创建2D物理材质为光滑的wall
    public PhysicsMaterial2D wall;


    [Header("状态")]
    
    //创建bool判断玩家是否受击
    public bool isHurt;
    //创建bool判断玩家是否死亡
    public bool isDead;
    //创建bool判断玩家是否攻击
    public bool isAttack;

    //awake是在一开始要做的部分
    private void Awake(){
        //将玩家的刚体组件赋给rb
        rb = GetComponent<Rigidbody2D>();
        //将玩家的物理检测组件赋给physicscheck
        physicscheck = GetComponent<PhysicsCheck>();
        //将玩家的动画组件赋给playeranimation
        playeranimation = GetComponent<PlayerAnimation>();
        //将玩家的胶囊碰撞体组件赋给coll
        coll = GetComponent<CapsuleCollider2D>();

        //实例化一个inputcontrol
        inputControl = new PlayerInputControl();
        //注册一个函数叫jump
        inputControl.Gameplay.Jump.started += Jump;
        //注册一个函数叫attack
        inputControl.Gameplay.Attack.started += PlayerAttack;
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
        //调用检查状态函数
        CheckState();
    }

    //固定频率更新
    private void FixedUpdate(){
        //如果不受击或不攻击则进行移动
        if(!isHurt && !isAttack)
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

    //创建玩家attack的函数方法
    private void PlayerAttack(InputAction.CallbackContext obj){
        //播放一下攻击的动画
        playeranimation.PlayerAttack();
        //更改为攻击状态
        isAttack = true;
    }

    #region UnityEvent

    //创建gethurt函数方法，实现受击反弹的效果
    public void GetHurt(Transform attacker){
        //成功受击
        isHurt = true;
        //使刚体rb在x轴与y轴的速度都变为0，把惯性停下来
        rb.velocity = Vector2.zero;
        //计算受击反弹方向（用玩家坐标减去敌人坐标，以正负来定夺方向）"进行归一化“
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;

        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }

    //创建玩家死亡时无法继续操作控制的函数方法
    public void PlayerDead(){
        //先让玩家死掉
        isDead = true;
        ///直接在输入系统里面取消玩家的游玩控制权（disable）
        inputControl.Gameplay.Disable();
    }

    #endregion

    //创建函数checkstate检查当前人物状态
    private void CheckState(){
        //检测是否在地面从而判断是否需要切换材质
        coll.sharedMaterial = physicscheck.isGround?normal:wall;
    }
}

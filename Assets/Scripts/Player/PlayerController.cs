using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{/*笔记：private类型的变量只能在当前类（class）中使用，而public则可以在其他class中被调用，且可以在Inspect窗口中看到*/
    public UnityEvent OnDie;

    public PlayerInputControl inputControl;

    private Rigidbody2D rb;//rb是刚体的类型

    private PhysicsCheck physicsCheck;//private类型的变量只能在这个类中被调用

    private Character character;

    [Header("输入的移动方向")]
    public Vector2 inputDirection;//用来存储Vector2类型的变量,是用于储存输入的方向

    [Header("基本参数")]
    public float speed;//速度的值

    public float jumpForce;//跳跃的力

    public float exitTime = 300;

    public bool isHurt;//判断是否受伤

    public bool isDead;//判断是否处于死亡状态

    public float hurtForce;//受伤时受到的冲击力

    private void Awake(){
        rb=GetComponent<Rigidbody2D>();//变量的赋值
        physicsCheck=GetComponent<PhysicsCheck>();
        character=GetComponent<Character>();

        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started +=/*+=就是注册一个函数*/ Jump;//跳跃相关
    }



    private void OnEnable(){//执行此函数时使inputControl函数启用
        inputControl.Enable();
    }

    private void OnDisable()
    {//执行此函数时使inputControl函数禁用
        inputControl.Disable();

    }
    public void ExitTimeCounter()
    {
        
        for (; exitTime >= 0;)
        {
            exitTime-=Time.deltaTime;
        }
        
            ExitGame();
        
    }
    private void Update(){
        inputDirection/*用于储存输入的方向*/= inputControl.Gameplay.Move/*GamePlay里的Actions*/.ReadValue/*读取数值*/<Vector2/*被读取数值的类型*/>();
        //持续获得玩家输入的移动方向
        if (character.currentHealth <= 0)
        {
            rb.velocity=new Vector2(0,0);
            OnDie?.Invoke();/*触发死亡的函数*/


        }
        if(transform.position.y < -11.5) {
            rb.velocity = new Vector2(0, 0);
            OnDie?.Invoke();/*触发死亡的函数*/
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void FixedUpdate(){//跟物理有关的都放在FixedUpdate执行
        if (!isHurt)//如果没有处于受伤状态
        Move();//人物移动
    }

    public void Move()
    {
        //人物移动
        rb.velocity=new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);

        //实现人物的翻转（转向）
        int faceDir=(int)transform.localScale.x;
        if(inputDirection.x>0)faceDir=1;
        else if(inputDirection.x<0)faceDir=-1;
        

        //人物翻转
        transform.localScale =new Vector3(faceDir,1,1);
    }


    private void Jump (InputAction. CallbackContext obj){
        //Debug.Log("JUMP");
        if(physicsCheck.isGround)
        rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);//跳跃，施加一个向上的脉冲力
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity=Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;

        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);//水平被击飞的力
        rb.AddForce(transform.up * hurtForce*0.7f, ForceMode2D.Impulse);//竖直方向被击飞的力
        //transform.up怎么使用突然忘了，记得到跳跃的实现中再看看！！
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();//使输入中游玩部分的操控被关闭
    }
}

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
    //创建刚体rb（指玩家）
    private Rigidbody2D rb;
    //创建二维向量表输入方向
    public Vector2 inputDirection;
    //创建速度（浮点类型）
    public float speed;

    //实例化
    private void Awake(){
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
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

    private void Update(){
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate(){
        Move();
    }

    public void Move(){
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if(inputDirection.x >0)
            faceDir = 1;
        if(inputDirection.x <0)
            faceDir = -1;

        //人物翻转
        transform.localScale = new Vector3(faceDir,1,1);
    }
}

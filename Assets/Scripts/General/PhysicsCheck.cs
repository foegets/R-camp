using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{   
    //获取胶囊碰撞体的组件
    private CapsuleCollider2D coll;

    [Header("检测参数")]
    //手动
    public bool manual;
    //创建二维向量：脚底位移差值
    public Vector2 bottomOffset;
    //创建二维向量，左边墙的偏移值与右边墙的偏移值
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    //创建checkraduis表示检测范围 
    public float checkRaduis;
    //创建变量groundlayer表示地面层
    public LayerMask groundLayer;

    [Header("状态")]
    //创建变量isground判断人物是否在地面上
    public bool isGround;
    //创建变量isground判断人物是否撞左边墙
    public bool touchLeftWall;
    //创建变量isground判断人物是否撞右边墙
    public bool touchRightWall;
    
    private void Awake()
    {
        //将胶囊碰撞体组件给到coll
        coll = GetComponent<CapsuleCollider2D>();

        //如果是自动的话捏就自动捏
        if(!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x)/2 + coll.offset.x, coll.bounds.size.y /2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    private void Update(){
        Check();
    }
    //
    //创建函数方法check
    public void Check(){
        //检测地面(括号内依次表示中心点，半径（检测范围），碰到的东西)
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRaduis,groundLayer);

        //检测墙体
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset,checkRaduis,groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset,checkRaduis,groundLayer);
    }

    //选中物体时才会划线的函数方法
    private void OnDrawGizmosSelected()
    {
        //绘画一个虚线球
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset,checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset,checkRaduis);
        
    }
}

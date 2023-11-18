using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public CapsuleCollider2D coll;
    [Header("检测参数")]
    public float checkRaduis;//创建范围变量，参数
    public LayerMask groundLayer;//筛选器，选地面做检测对象，参数
    public Vector2 bottonOffset;//调整检测圆的圆心位置，用来确定碰撞的实际检测范围，一个含有xy两个坐标的变量
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    [Header("状态")]
    public bool isGround;//创建用于判断是否与地面碰撞的变量
    //用于判断是否贴墙（为后面设计贴墙转身和动作转换做准备）
    public bool isLeftWall;
    public bool isRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        check();
    }
    public void check()
    {
        //地面,墙壁检测（以圆形判定，重载：同函数不同参,第一个参数包括位置变化和脚底位移差值）碰撞则等式为true
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottonOffset,checkRaduis,groundLayer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }


    
    //到底isGround的检测范围多大，通过unity自带绘制gizmos,以圆形表示，手动绘制
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottonOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}

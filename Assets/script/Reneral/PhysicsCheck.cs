using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public float checkRaduis;//创建范围变量，参数
    public LayerMask groundLayer;//筛选器，选地面做检测对象，参数
    public Vector2 bottonOffset;//脚底位移差值，用来确定碰撞的实际检测范围
    [Header("状态")]
    public bool isGround;//创建用于判断是否与地面碰撞的变量
    private void Update()
    {
        check();
    }
    public void check()
    {
        //地面检测（以圆形判定，重载：同函数不同参,第一个参数包括位置变化和脚底位移差值）碰撞则等式为true
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottonOffset,checkRaduis,groundLayer);
    }
    //在适当时机绘制gizmos,以圆形表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottonOffset, checkRaduis);
    }
}

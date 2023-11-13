using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{   
    [Header("检测参数")]
    //创建二维向量：脚底位移差值
    public Vector2 bottomOffset;
    //创建checkraduis表示检测范围 
    public float checkRaduis;
    //创建变量groundlayer表示地面层
    public LayerMask groundLayer;

    [Header("状态")]
    //创建变量isground判断人物是否在地面上
    public bool isGround;
    
    private void Update(){
        Check();
    }

    //创建函数方法check
    public void Check(){
        //检测地面(括号内依次表示中心点，半径（检测范围），碰到的东西)
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRaduis,groundLayer);
    }

    //选中物体时才会划线的函数方法
    private void OnDrawGizmosSelected()
    {
        //绘画一个虚线球
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
        
    }
}

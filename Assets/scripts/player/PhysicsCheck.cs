using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public Vector2 bottomOffset;//检测范围中心偏移值


    public float checkRaduis;//检测范围大小

    [Header("状态")]
    public bool isGround;//是否处于地面

    public LayerMask groundLayer;
    private void Update()
    {
        Check();
    }

    public void Check()//检测处于地面的状态
    {
        
         isGround=Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset,checkRaduis,groundLayer);
    }

    private void OnDrawGizmosSelected()//检测范围绘制
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public Vector2 bottomOffset;
    public float checkRaduis;


    public LayerMask groundLayer;

    [Header("状态")]
    public bool isGround;
    public int canJumpCount = 2;// 可跳跃次数
    public bool isDoubleJump = true;
    public bool isJump = true;  
    public int jumpCount; // 跳跃次数


    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}

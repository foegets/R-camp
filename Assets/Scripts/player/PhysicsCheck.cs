using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public Vector2 bottomOffest;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("状态")]
    //检测当前人物状态,是否在地面
    public bool isGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffest, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffest,checkRaduis);
    }
}

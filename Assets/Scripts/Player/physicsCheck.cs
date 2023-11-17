using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public Vector2 bottomOffset;
    public float checkRaduis;
    public float wallCheckDistance;

    public Transform wallCheck;

    public LayerMask groundLayer;

    [Header("状态")]
    public bool isGround;
    public bool isWalking;
    public bool isTouchingWall;
    public bool isWallSliding;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        //检测滑墙
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance,groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}

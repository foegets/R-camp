using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public Vector2 bottomOffset;//检测范围中心偏移值


    public float checkRaduis;//检测范围大小

    public float rushDuration;
    private float rushTime;
    public int rushNum;

    [Header("状态")]
    public bool isGround;//是否处于地面

    public bool isPlayform;

    public bool isrushReady=true;

    public bool isRush;

    public bool isdead;

    public LayerMask groundLayer;

    private void Start()
    {
        rushTime = rushDuration;
        rushNum = 1;
    }
    private void Update()
    {
        CheckIsground();
        CheckIsRushReady();
    }

    private void FixedUpdate()
    {
        CheckisRush();
    }

    public void CheckIsground()//检测处于地面的状态
    {
        
         isGround=Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset,checkRaduis,groundLayer);
        isPlayform=Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis,9);
    }

    private void CheckIsRushReady()
    {
        if (isGround == false && rushNum <= 0)
            isrushReady = false;
        if (isGround == true)
        {
            isrushReady = true;
            rushNum = 1;
        }
    }

    private void OnDrawGizmosSelected()//检测范围绘制
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
    }

    private void CheckisRush()
    {
        if(isRush)
        {
            
            rushTime -= Time.fixedDeltaTime;
            if (rushTime <= 0)
            {
                isRush = false;
                rushTime = rushDuration;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public Vector2 bottomOffset;//��ⷶΧ����ƫ��ֵ


    public float checkRaduis;//��ⷶΧ��С

    public float rushDuration;
    private float rushTime;
    public int rushNum;

    [Header("״̬")]
    public bool isGround;//�Ƿ��ڵ���

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

    public void CheckIsground()//��⴦�ڵ����״̬
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

    private void OnDrawGizmosSelected()//��ⷶΧ����
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

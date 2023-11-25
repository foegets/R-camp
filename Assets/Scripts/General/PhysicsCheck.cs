using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("������")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftwallOffset;
    public Vector2 rightwallOffset;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("״̬")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();

        if (!manual)
        {
            rightwallOffset = new Vector2 ((coll.bounds.size.x + coll.offset.x)/2, coll.bounds.size.y/2);
            leftwallOffset = new Vector2 (-rightwallOffset.x, rightwallOffset.y);
        }
    }

    public void Update()
    {
        Check();
    }

    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

        //ǽ���ж�
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftwallOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightwallOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        //��ʾ����
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftwallOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightwallOffset, checkRaduis);
    }
}

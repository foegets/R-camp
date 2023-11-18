using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public Vector2 bottomOffest;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("״̬")]
    //��⵱ǰ����״̬,�Ƿ��ڵ���
    public bool isGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffest, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffest,checkRaduis);
    }
}

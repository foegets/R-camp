using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    public Vector2 bottomOffset;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("״̬")]
    public bool isGround;

    public void Update()
    {
        Check();
    }

    public void Check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
         Gizmos.DrawWireSphere(transform.position, checkRaduis);
    }
}

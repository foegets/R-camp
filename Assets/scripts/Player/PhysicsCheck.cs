using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround;
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector3 bottomOffset;
    private void Update(){
        Check();
    }
    public void Check(){
        //检测地面

        isGround = Physics2D.OverlapCircle(transform.position + bottomOffset,checkRadius,groundLayer);
    }
    private void OnDarwGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position + bottomOffset,checkRadius);
    }
}

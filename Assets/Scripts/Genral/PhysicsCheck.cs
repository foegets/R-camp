using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundLayer;//ºÏ≤‚µÿ√Ê
    public bool isGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        isGround =Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, checkRadius, groundLayer);          
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);
    }
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRaduis;
    public bool isGround; 
    public LayerMask groundLayer;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle(transform.position,checkRaduis,groundLayer);
    }
}

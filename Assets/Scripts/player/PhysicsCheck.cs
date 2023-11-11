using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;
    private void Update()
    {
        check();
    }
    public void check()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, groundLayer);
    }
}

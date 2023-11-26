using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;
    public bool isMovable;
    public bool isJumpable;
  
    public LayerMask platLayer;

    private void Update()
    {
        Check();
        
    }
    public void Check()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, groundLayer);
        
    }

    public void UnJumpable()
    {
        isJumpable = false;
    }
    
    public void UnMovable()
    {
        isMovable = false;  
    }

    public void Moveable()
    {
        isMovable = true;
    }

    public void Jumpable()
    {
        isJumpable = true;
    }


}

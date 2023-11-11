using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    public bool isTouch;
    public float checkRadius;
    public LayerMask groundLayer;
    public Vector3 bottomOffset;
    public void Check(){
        //检测地面

        isTouch = Physics2D.OverlapCircle(transform.position + bottomOffset,checkRadius,groundLayer);
    }
}

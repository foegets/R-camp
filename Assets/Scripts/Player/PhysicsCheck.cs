using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRaduis;//检测范围
    public bool isGround;//判断是否在地面
    public LayerMask groundLayer;//地面层级
    public void Check()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, groundLayer);
        //检查碰撞体是否在一个圆区域
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
}

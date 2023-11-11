using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class physicalcheck : MonoBehaviour
{
    Vector3 posotion1 =new Vector3(0, 10, 0);
    private Rigidbody2D rb;
    public Vector2 bottomOffset;//脚底的位移差值
    [Header("基本参数")]
    public float checkRaduis;
    public bool isGround;
    public LayerMask groundlayer;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        check();
       
    }
    private void FixedUpdate()
    {
        Return1();
    }

    public void check()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis);


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
    private void Return1()
    {
        if (transform.position.y <= -100)
            transform.position = posotion1;
     
    }
}

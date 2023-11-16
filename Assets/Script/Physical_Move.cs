using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physical_Move : MonoBehaviour
{
    // 移动速度
    public float movespeed = 3f;
    
    Vector3 move = new Vector3();
    // 设置阻力系数  
    public float stopforce = 1.5f;

    Rigidbody my_rb;
    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody>();
        
        move = Vector3.zero;
    }
    void FixedUpdate()
    {
        // 基础移动

        if (move != Vector3.zero)
        {
            my_rb.AddForce(move * movespeed);
        }
        else
        {
            my_rb.AddForce(-my_rb.velocity * stopforce);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // 获取要移动的x和z方向
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");
        move = new Vector3(movex, 0, movez);
    }
}

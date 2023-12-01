using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physical_Jump : MonoBehaviour
{
    // 获取刚体组件
    Rigidbody my_rb;
    // 跳跃施加的瞬时力度
    public float jumpforce = 5f;
    // 判断是否处于跳跃状态
    public bool isjumping = false;
    //// 设置判断落地的距离条件
    //public float isGroundHeight = 0.05f;
    //// 获取地面的层级掩码
    //public LayerMask GroundLayer;
    //// 射线检测距离
    //public float RayLen;
    //// 计算玩家与地面的距离
    //public float ToGroundDistance;
    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody>();
        //ToGroundDistance = 0;
    }
    void FixedUpdate()
    {
        //CheckGround();
    }
    
    // Update is called once per frame
    void Update()
    {
        // 基础跳跃
        if (!isjumping && Input.GetKeyDown(KeyCode.Space))
        {
            my_rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isjumping = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isjumping = false;
        }

    }
    //public void CheckGround()
    //{
    //    // 获取射线碰撞对象信息
    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, RayLen, GroundLayer))
    //    {
    //        ToGroundDistance = Vector3.Distance(transform.position, hitInfo.transform.position);
    //        if (ToGroundDistance <= isGroundHeight)
    //        {
    //            isjumping = false;
    //        }
    //    }
    //}
}

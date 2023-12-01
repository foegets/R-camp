using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped_Door : MonoBehaviour
{
    // 旋转速度
    public float RotSpeed = 30f;
    // 击飞力度
    public float hitForce = 50.0f;
    // 判断是否发生碰撞
    public bool isCollid;
    // 击飞方向
    Vector3 hitDir;
    // 获取玩家组件
    Rigidbody rb;
    CharacterController playerController;
    // Start is called before the first frame update
    void Start()
    {
        isCollid = false;
    }

    private void FixedUpdate()
    {
        if (isCollid)
        {
            if (rb != null)
            {
                rb.AddForce(hitDir.normalized * hitForce, ForceMode.Impulse);
            }
            else if (playerController != null)
            {
                // 不知道,我只知道SimpleMove卵用没有
            }
            else
            {
                print("没有检测到有效的碰撞组件");
            }
            isCollid = false;
        }
    }
    void Update()
    {
        Vector3 CurAngle = transform.eulerAngles;
        CurAngle.y += RotSpeed * Time.deltaTime;
        transform.eulerAngles = CurAngle;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            playerController = collision.gameObject.GetComponent<CharacterController>();
            // 获得撞击点的运动方向并用作撞击方向
            hitDir = rb.GetPointVelocity(collision.contacts[0].point);
            hitDir.y = 0;
            isCollid = true;
        }
    }


    
 
}

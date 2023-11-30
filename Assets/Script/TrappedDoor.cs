using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped_Door : MonoBehaviour
{
    // 旋转速度
    public float RotSpeed = 30f;
    // 击飞力度
    public float hitForce = 50.0f;
    // 获取玩家组件
    Rigidbody rb;
    CharacterController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        
    }
    void Update()
    {
        Vector3 CurAngle = transform.eulerAngles;
        CurAngle.y += RotSpeed * Time.deltaTime;
        transform.eulerAngles = CurAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            playerController = other.gameObject.GetComponent<CharacterController>();
            Vector3 HitDir = transform.position - other.transform.position + Vector3.up;
            if (rb != null)
            {
                rb.AddForce(HitDir * hitForce, ForceMode.Impulse);
            }
            else if (playerController != null)
            {

                playerController.SimpleMove(HitDir * hitForce);
            }
            else
            {
                Debug.Log("玩家身上没有有效的碰撞组件");
            }
        }
    }
 
}

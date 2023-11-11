using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物理跳跃 : MonoBehaviour
{
    Rigidbody my_rb;
    // 跳跃施加的瞬时力度
    public float jumpforce = 5f;
    // 判断是否处于跳跃状态
    bool if_jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // 基础跳跃
        if (!if_jumping && Input.GetButtonDown("Jump"))
        {
            my_rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if_jumping = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if_jumping = false;
    }
}

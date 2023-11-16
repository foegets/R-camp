using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToChangeDirectionOnPlayer : MonoBehaviour
{
    // 敏感度
    public float sensitivity = 50f;
    // 限制转向角度
    float Xrot = 0f;
    float Yrot = 0f;
    // 获得刚体组件
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 锁定并隐藏光标
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        Xrot -= mouseY;
        Xrot = Mathf.Clamp(Xrot, -75f, 75f);
        Yrot = transform.eulerAngles.y + mouseX * sensitivity;
        transform.rotation = Quaternion.Euler(Xrot, Yrot, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 鼠标转向_360度 : MonoBehaviour
{
    Rigidbody my_rb;
    // 旋转速度
    public float rotatespeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // 基础转向
        float rotationAngle = Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
        transform.rotation *= rotation;
        // 乘于Time.deltaTime以获得平滑和一致的旋转动画
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

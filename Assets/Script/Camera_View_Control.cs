using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera_View_Control : MonoBehaviour
{  

    void Start()
    {
        
    }
    
    void Update()
    {
        // 判断鼠标右键是否输入
        if (Input.GetMouseButton(1))
        {
            //// 通过鼠标拖拽改变摄像机位置  
            //Vector3 moveDirection = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            //transform.position -= moveDirection * dragSpeed * Time.deltaTime;

            //// 保持摄像机与目标的距离不变  
            //float currentDistance = Vector3.Distance(transform.position, target.position);
            //float scaleFactor = currentDistance / initialDistance;
            //transform.position = target.position + offset * scaleFactor;
        }
    }
}

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
        // �ж�����Ҽ��Ƿ�����
        if (Input.GetMouseButton(1))
        {
            //// ͨ�������ק�ı������λ��  
            //Vector3 moveDirection = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
            //transform.position -= moveDirection * dragSpeed * Time.deltaTime;

            //// �����������Ŀ��ľ��벻��  
            //float currentDistance = Vector3.Distance(transform.position, target.position);
            //float scaleFactor = currentDistance / initialDistance;
            //transform.position = target.position + offset * scaleFactor;
        }
    }
}

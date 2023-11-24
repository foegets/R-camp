using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//�ýű�������virtual camera���ÿ������������ƶ���Χ
public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();    
    }

    private void Start()
    {
        GetNewCameraBounds();
    }
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");//��ȡ��ǩΪbounds��object
        if (obj == null)
            return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();//ÿ�θ����������������
    }
}

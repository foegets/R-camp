using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

//�ýű�������virtual camera���ÿ������������ƶ���Χ
public class CameraControl : MonoBehaviour
{
    [Header("�¼�����")]
    public VoidEventSO afterSceneLoadedEvent;
    private CinemachineConfiner2D confiner2D;
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();    
    }
    private void OnEnable()
    {
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        GetNewCameraBounds();
    }

    private void OnDisable()
    {
        afterSceneLoadedEvent.OnEventRaised = OnAfterSceneLoadedEvent;
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

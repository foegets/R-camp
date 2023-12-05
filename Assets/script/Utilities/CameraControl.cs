using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

//该脚本用于让virtual camera获得每个场景的相机移动范围
public class CameraControl : MonoBehaviour
{
    [Header("事件监听")]
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
        var obj = GameObject.FindGameObjectWithTag("Bounds");//获取标签为bounds的object
        if (obj == null)
            return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();//每次更换场景后都清除缓存
    }
}

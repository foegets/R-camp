using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO afterSceneLoadedEvent;

    //获取一下相机的这个组件
    private CinemachineConfiner2D confiner2D;

    //添加一下相机抖动代码
    public CinemachineImpulseSource impulseSource;

    //监听一下这个事件捏
    public VoidEventSO cameraShakeEvent;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    //注册函数
    private void OnEnable()
    {
        //我听到了！我要注册函数执行！
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    //用完记得注销嗷
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }

    //
    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    //场景切换后更改

    private void OnAfterSceneLoadedEvent()
    {
        GetNewCameraBounds();
    }

    //获取新的相机边界
    private void GetNewCameraBounds()
    {
        //临时创建一个obj变量，找到标签为“Bounds”的object，赋给boj；
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        //如果不存在这个object，就结束函数
        if(obj == null)
            return;
        //组件存在，则获取边界的碰撞体（基类coll）组件，给相机；
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        //清除缓存
        confiner2D.InvalidateCache();
    }
}

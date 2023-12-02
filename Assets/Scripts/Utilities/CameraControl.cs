using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO afterSceneLoadedEvent;

    private CinemachineConfiner2D confiner2D;
    public GameObject obj;
    private void Awake()
    {
        confiner2D=GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }
    private void OnAfterSceneLoadedEvent()
    {
        GetNewCameraBounds();
    }
    //private void Start()
    //{
    //    GetNewCameraBounds();
    //}
    private void GetNewCameraBounds()
    {
        obj = GameObject.FindGameObjectWithTag("Bounds");
        Debug.Log(obj.name);
        Debug.Log(obj);
        if (obj = null) return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();
    }
}

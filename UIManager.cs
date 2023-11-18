using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public CharacterEventSO healthEvent;
    private void OnEnable()//注册事件
    {
        healthEvent.OnEventRaised += OnHealthEvent;

    }
    private void OnDisable()//取消注册
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        throw new NotImplementedException();
    }
}

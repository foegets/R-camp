using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public CharacterEventSO healthEvent;
    private void OnEnable()//ע���¼�
    {
        healthEvent.OnEventRaised += OnHealthEvent;

    }
    private void OnDisable()//ȡ��ע��
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        throw new NotImplementedException();
    }
}

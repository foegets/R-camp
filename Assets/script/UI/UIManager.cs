using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
//�ô�����������calculate�����Ĺ㲥���Ըýű�Ϊ�н鴫�ݸ����ж�����

public class UIManager : MonoBehaviour
{
    //ȡ�ýű���������OnHealthEvent���������ݴ���
    public PlayerStatBar playerStatBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    //ע���¼�
    private void OnEnable()
    {
        //��һ������Ϊ�ýű�ǰ���CharacterEventSO�����֣��ڶ�������Ϊ���ķ�����������OnEventRaised�¼���+=��ʾ���ģ������Ϊ�Լ�д��������,��һ������
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    //ע��Ϊ-=
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Calculate calculate)
    {
        var percentage = calculate.currentHealth / calculate.maxHealth;
        playerStatBar.OnHealthChange(percentage);
    }
}

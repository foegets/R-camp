using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor.AddressableAssets.Build.Layout;
using UnityEngine;
//�ô�����������calculate�����Ĺ㲥���Ըýű�Ϊ�н鴫�ݸ����ж�����

public class UIManager : MonoBehaviour
{
    //ȡ�ýű���������OnHealthEvent���������ݴ���
    public PlayerStatBar playerStatBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO LoadEvent;
    //ע���¼�
    private void OnEnable()
    {
        //��һ������Ϊ�ýű�ǰ���CharacterEventSO�����֣��ڶ�������Ϊ���ķ�����������OnEventRaised�¼���+=��ʾ���ģ������Ϊ�Լ�д��������,��һ������
        healthEvent.OnEventRaised += OnHealthEvent;
        LoadEvent.LoadRequestEvent += OnLoadEvent;
    }

    //ע��Ϊ-=
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        LoadEvent.LoadRequestEvent -= OnLoadEvent;
    }

    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = sceneToLoad.sceneType == AssetType.Scene;
        playerStatBar.gameObject.SetActive(!isMenu);

    }

    private void OnHealthEvent(Calculate calculate)
    {
        var percentage = calculate.currentHealth / calculate.maxHealth;
        playerStatBar.OnHealthChange(percentage);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor.AddressableAssets.Build.Layout;
using UnityEngine;
//该代码用来接收calculate发出的广播，以该脚本为中介传递给所有订阅者

public class UIManager : MonoBehaviour
{
    //取得脚本用于下面OnHealthEvent函数的数据传递
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO LoadEvent;
    //注册事件
    private void OnEnable()
    {
        //第一个单词为该脚本前面的CharacterEventSO的名字，第二个单词为订阅方法，即订阅OnEventRaised事件，+=表示订阅，后面的为自己写的新名字,是一个函数
        healthEvent.OnEventRaised += OnHealthEvent;
        LoadEvent.LoadRequestEvent += OnLoadEvent;
    }

    //注销为-=
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

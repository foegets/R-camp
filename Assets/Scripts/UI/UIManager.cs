using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //把这个百分比的值传递给玩家UI的那个物体
    public PlayerStatBar playerStatBar;
    //把金币的获得数传递给控制金币的UI
    public GoldCoinsBar goldCoinsBar; 

    [Header("事件监听")]
    //扣血了吗？？
    public CharacterEventSO healthEvent;
    //开始加载场景了吗？？
    public SceneLoadEventSO loadEvent;
    //捡到金币了吗？？
    public VoidEventSO pickCoinEvent;

    //注册事件（调用health event里的那个事件名字）
    private void OnEnable()
    {
        //把OnHealthEvent函数加入到这个事件里边
        healthEvent.OnEventRaised += OnHealthEvent;
        loadEvent.LoadRequestEvent += OnLoadEvent;
        pickCoinEvent.OnEventRaised += OnPickCoinEvent;
    }

    //把这个函数从这个事件里面注销掉捏
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        loadEvent.LoadRequestEvent -= OnLoadEvent;
        pickCoinEvent.OnEventRaised -= OnPickCoinEvent;
    }

    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        //如果进到菜单了，玩家的UI就不显示
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
            playerStatBar.gameObject.SetActive(!isMenu);
            goldCoinsBar.gameObject.SetActive(!isMenu);
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }

    private void OnPickCoinEvent()
    {
        goldCoinsBar.OnPickCoin();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //把这个百分比的值传递给玩家UI的那个物体
    public PlayerStatBar playerStatBar;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;

    //注册事件（调用health event里的那个事件名字）
    private void OnEnable()
    {
        //把OnHealthEvent函数加入到这个事件里边
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    //把这个函数从这个事件里面注销掉捏
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
}

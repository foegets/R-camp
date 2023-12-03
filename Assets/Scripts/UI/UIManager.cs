using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;//监听广播的信息（即血量的变化）


    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent/*在Character.cs中*/;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage =character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
}

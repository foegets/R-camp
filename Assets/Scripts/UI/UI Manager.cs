using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayStatBar playStatBar;

    [Header("事件监听")]

    public CharacterEventSO healthEvent;

    public void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playStatBar.OnHealthChange(persentage);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvent;//�����㲥����Ϣ����Ѫ���ı仯��


    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent/*��Character.cs��*/;
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

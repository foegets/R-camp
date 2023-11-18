using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public PlayerStatBar PlayerStatBar;//获得物体
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
            var persentage = character.currentHealth / character.maxHealth;
            PlayerStatBar.OnHealthChange(persentage);//获得数值的变化和填充
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public PlayerStatBar PlayerStatBar;//�������
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
            var persentage = character.currentHealth / character.maxHealth;
            PlayerStatBar.OnHealthChange(persentage);//�����ֵ�ı仯�����
        }
    }
}
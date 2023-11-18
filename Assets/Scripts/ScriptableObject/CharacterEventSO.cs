using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharaterEventSO")]//创建文件
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}


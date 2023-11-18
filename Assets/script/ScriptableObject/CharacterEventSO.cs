using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//�����ʲ��ļ�������menuNameΪ�˵���,EventΪ·����CharacterΪ�ʲ�����
[CreateAssetMenu(menuName ="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    //����calculate����,OnEventRaisedΪ����
    public UnityAction<Calculate> OnEventRaised;


    //���������¼���calculate���봫��ȥ������һ���¼��Ķ��ķ�ʽ
    public void RaiseEvent(Calculate calculate)
    {
        OnEventRaised?.Invoke(calculate);
    }
}

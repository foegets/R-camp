using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ýű����ڳ���ת����ж�س����������µĳ���,��playerһ��λ��
public class TeleportPoint : MonoBehaviour, IInteractable
{
    //����¼�
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO SceneToGo;
    public Vector3 PositionToGo;
    public void TriggerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(SceneToGo, PositionToGo, true);
    }
    
}

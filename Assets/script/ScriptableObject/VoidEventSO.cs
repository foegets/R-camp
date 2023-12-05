using UnityEngine.Events;
using UnityEngine;
[CreateAssetMenu(menuName = "Event/VoidEventSO")]
//û�з���ֵ��SO
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;
    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}

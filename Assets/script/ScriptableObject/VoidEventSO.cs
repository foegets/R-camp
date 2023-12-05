using UnityEngine.Events;
using UnityEngine;
[CreateAssetMenu(menuName = "Event/VoidEventSO")]
//没有返回值的SO
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;
    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}

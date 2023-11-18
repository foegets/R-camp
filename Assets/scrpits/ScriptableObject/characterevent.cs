using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName ="Event/characterevent")]
public class characterevent : ScriptableObject
{
    public UnityAction<character> ONEventRaised;

    public void RaiseEvent(character character)
    {
        ONEventRaised?.Invoke(character);

    }
    
}

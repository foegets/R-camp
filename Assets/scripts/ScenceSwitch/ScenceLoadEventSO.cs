using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName ="Event/ScenceLoadEventSO")]
public class ScenceLoadEventSO : MonoBehaviour
{
    public UnityAction<GameScenceSO, Vector3, bool> LoadRequestEvent;
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo">Player������</param>
    /// <param name="fadescreen">�Ƿ��뽥��</param>
    public void RaiseLoadRequestEvent(GameScenceSO locationToLoad,Vector3 posToGo,bool fadescreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadescreen);
    }

}

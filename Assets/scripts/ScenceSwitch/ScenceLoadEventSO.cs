using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName ="Event/ScenceLoadEventSO")]
public class ScenceLoadEventSO : MonoBehaviour
{
    public UnityAction<GameScenceSO, Vector3, bool> LoadRequestEvent;
    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="posToGo">Player的坐标</param>
    /// <param name="fadescreen">是否渐入渐出</param>
    public void RaiseLoadRequestEvent(GameScenceSO locationToLoad,Vector3 posToGo,bool fadescreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadescreen);
    }

}

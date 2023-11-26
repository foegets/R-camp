using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]

public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="locationToLoad">Ҫȥ�ĳ���</param>
    /// <param name="posToGo">player��Ŀ������</param>
    /// <param name="fadeScreen">�Ƿ�����Ļ���뽥��Ч��</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadeScreen);   //�����ʲô��˼
    }
}

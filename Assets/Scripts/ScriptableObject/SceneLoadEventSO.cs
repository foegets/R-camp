using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]

public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad">要去的场景</param>
    /// <param name="posToGo">player的目的坐标</param>
    /// <param name="fadeScreen">是否有屏幕渐入渐出效果</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadeScreen);   //这段是什么意思
    }
}

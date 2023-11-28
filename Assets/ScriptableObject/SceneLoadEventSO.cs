using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    //创建一个事件，要传进去一个场景（要加载的场景），传进去一个坐标（玩家在下一个地图的初始坐标）,传入一个布尔值（表示加载场景是否需要播放渐入渐出的效果）,起名叫做“场景加载请求事件”
    public UnityAction<GameSceneSO,Vector3,bool> LoadRequestEvent;

    //呼叫！（启动方法
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fadeScreen);
    }
}

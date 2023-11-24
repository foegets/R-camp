using UnityEngine;
using UnityEngine.Events;

//该脚本作为事件，用于传递所去场景，所去坐标，是否需要场景淡入淡出效果等
[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    //创建事件，传进的变量参数有场景，坐标，bool值判断是否需要淡入淡出
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
    //呼叫，启动事件的方法,启动上面的LoadRequestEvent,locationToLoad为要加载的场景，posToGo为player的目的坐标，fedeScreen为场景的淡入淡出
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad,Vector3 posToGo,bool fedeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, posToGo, fedeScreen); 
    }
}

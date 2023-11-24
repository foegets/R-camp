using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//该脚本用于场景转换，卸载场景，加载新的场景,给player一个位置
public class TeleportPoint : MonoBehaviour, IInteractable
{
    //添加事件
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO SceneToGo;
    public Vector3 PositionToGo;
    public void TriggerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(SceneToGo, PositionToGo, true);
    }
    
}

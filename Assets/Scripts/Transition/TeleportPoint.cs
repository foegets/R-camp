using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    //添加好这个事件
    public SceneLoadEventSO loadEventSO;
    //要知道，我要去哪个场景，所以要创建这个
    public GameSceneSO sceneToGo;
    //记录坐标
    public Vector3 positionToGo;

   public void TriggerAction()
   {
    Debug.Log("传送！");

    //呼叫一下他，启动一下加载的请求(广播！)
    loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);

   }
}

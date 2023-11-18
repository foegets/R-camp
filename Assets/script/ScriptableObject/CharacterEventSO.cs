using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//创建资产文件的描述menuName为菜单名,Event为路径，Character为资产名字
[CreateAssetMenu(menuName ="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    //调用calculate代码,OnEventRaised为名字
    public UnityAction<Calculate> OnEventRaised;


    //想启动该事件则将calculate代码传进去，这是一个事件的订阅方式
    public void RaiseEvent(Calculate calculate)
    {
        OnEventRaised?.Invoke(calculate);
    }
}

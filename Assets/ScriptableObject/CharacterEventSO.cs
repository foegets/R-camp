using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    //character就是我们想传入的东西，OnEventRaised这个事件可以被任何代码订阅(只要订阅了该事件的代码，在广播的时候都会执行该事件)
    public UnityAction<Character> OnEventRaised;

    //创建函数方法启动这个事件(谁想启动这个事件就把自己的character代码传进去？？)
    public void RaiseEvent(Character character)
    {
        //事件订阅，调用
        OnEventRaised?.Invoke(character);
    }

}

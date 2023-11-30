using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]

public class FadeEventSO : ScriptableObject
{
    //创建一个事件
    public UnityAction<Color,float,bool> OnEventRaised;

    //逐渐变黑
    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black, duration, true);
    }

    //逐渐变透明
    public void FadeOut(float duration)
    {
        RaiseEvent(Color.clear, duration, false);
    }

    //生成事件的执行方法
    public void RaiseEvent(Color target,float duration,bool fadeIn)
    {
        OnEventRaised?.Invoke(target,duration,fadeIn);
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    [Header("事件监听")]
    //阿，原来是要开始渐入渐出了吗！
    public FadeEventSO fadeEvent;

    public Image fadeImage;

    //监听的人要好好注册函数方法应对广播呢
    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent;
    }


    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent;
    }

    public void OnFadeEvent(Color target,float duration,bool fadeIn)
    {
        fadeImage.DOBlendableColor(target,duration);
    }
}

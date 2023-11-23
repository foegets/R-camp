using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    //创建事件进行广播传递
    public PlayAudioEventSO playAudioEvent;
    //创建音频片段
    public AudioClip audioClip;
    //如果勾选了这个，即启动就播放（BGM）
    public bool playOnEnable;

    private void OnEnable()
    {
        if(playOnEnable)
            PlayAudioClip();
    }

    //创建单独的函数方法负责播放音乐片段
    public void PlayAudioClip()
    {
        //传入片段
        playAudioEvent.RaiseEvent(audioClip);
    }
}

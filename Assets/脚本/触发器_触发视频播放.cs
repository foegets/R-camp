using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class 触发器_触发视频播放 : MonoBehaviour
{
    // 要播放视频
    public VideoClip video;
    // 要播放的哪里
    public GameObject target;
    // 视频播放器
    VideoPlayer player;
    // 判断是否该播放
    bool ifhasplayed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (video == null)
        {
            Debug.LogError("没有有效的视频文件可以播放");
            return;
        }
        if (player == null)
        {
            Debug.LogError("没有有效的播放器对象可供播放");
            return;
        }
        // 设置播放器
        player = target.AddComponent<VideoPlayer>();
        // 设置要播放的视频
        player.clip = video;
        // 设置播放状态
        player.playOnAwake = false;
        // 准备视频播放
        player.Prepare();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!ifhasplayed)
        {
            ifhasplayed = true;
            player.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

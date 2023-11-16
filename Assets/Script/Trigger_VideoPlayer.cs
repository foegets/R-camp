using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class Trigger_VideoPlayer : MonoBehaviour
{
    // 要播放的哪里
    public GameObject target;
    // 视频播放器
    VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        
        // 设置播放器
        player = target.GetComponent<VideoPlayer>();
        // 设置播放状态
        player.playOnAwake = false;
        // 准备视频播放
        player.Prepare();
    }
    
    void OnTriggerEnter(Collider other)
    {
        player.Play();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

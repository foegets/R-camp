using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerGet : MonoBehaviour
{
    public Text coin;//文本，用来绑定ui
    public int coinCount;//拾取金币计数器
    public AudioSource source;//用于播放拾取金币音效
    public bool coinIsAllPicked;
    private void Start()
    {
        coinIsAllPicked = false;
    }
    private void Update()
    {
        if (coinCount==5)
            SceneManager.LoadSceneAsync(3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Coin")
        Destroy(collision.gameObject);
        source.Play();
        coinCount++;
        coin.text = "Coin:"+coinCount.ToString();//改变ui文本
        if(coinCount==5)
            coinIsAllPicked=true;//设置值判断通关
    }
}

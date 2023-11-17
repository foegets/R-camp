using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGet : MonoBehaviour
{
    public Text coin;//文本，用来绑定ui
    public int coinCount;//拾取金币计数器
    public AudioSource source;//用于播放拾取金币音效
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Coin")
        Destroy(collision.gameObject);
        source.Play();
        coinCount++;
        coin.text = "Coin:"+coinCount.ToString();
    }
}

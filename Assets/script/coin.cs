using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private GameObject coinTextUI;

    // Start is called before the first frame update
    void Start()
    {
        coinTextUI = GameObject.FindGameObjectWithTag("coinUI");//找到游戏对象
    }
    private void OnTriggerEnter2D(Collider2D other)//金币的触碰检测
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coinTextUI.GetComponent<coinUi>().currentCoinNum++;
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

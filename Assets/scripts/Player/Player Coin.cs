using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour
{
    public TMP_Text UI;
    public int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)//检测is trigger碰撞体进入
    {
        if (other.tag=="Coin" )   //检测进入的是不是coin//还可以用CompareTag("Coin")
        {   
            Score++;//分数+1
            UI.text = "Score:" + Score;//改变ui
            Destroy(other.gameObject);//coin消失

        }
    }
}

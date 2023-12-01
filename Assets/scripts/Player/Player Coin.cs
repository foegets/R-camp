using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(Score >= 8)
        {
            
            UI.text = "Game Clear!";
            SceneManager.LoadScene("MainScene");

        }
    }
    private void OnTriggerEnter2D(Collider2D other)//检测is trigger碰撞体进入
    {
        if (other.tag=="Coin" )   //检测进入的是不是coin//还可以用CompareTag("Coin")
        {   
            Score++;//分数+1
            UI.text = "Score:" + Score + "/8";
            Destroy(other.gameObject);//coin消失

        }
    }
}

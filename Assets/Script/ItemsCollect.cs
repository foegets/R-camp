using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCollect : MonoBehaviour
{
    public int addScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //检测拾取
        if (other.gameObject.CompareTag("Player"))
        {
            //得分增加
            ScoreUI.itemScore = ScoreUI.itemScore + addScore;
            //当接触到Player时物品消失
            Destroy(gameObject);
        }
    }
}

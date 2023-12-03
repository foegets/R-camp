using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen2 : MonoBehaviour
{
    public bool isGen;
    public GameObject prefaCherry;

    private UIController uIController;//引用实例
    void Start()
    {
        uIController = UIController.instance;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("player")) //判定碰撞体tag
        {
            if (isGen) 
            {
                Destroy(gameObject);
                LevelManeger.gensCollected++;

                GameObject cherry_0 = Instantiate(prefaCherry);// 建立预制体
                cherry_0.transform.position = new Vector2(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y+3);//改变预制体位置
            }
        }
    }



}

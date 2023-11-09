using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour//地雷脚本
{
    //弹飞参数
    public float ricochetOff = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("1");
        //进行炸飞和检测
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, ricochetOff);
            //检测是否和地雷接触并返回bool值
            player.Contactmine = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class trunkBullet : MonoBehaviour
{
    private GameObject trunkPosit;
    private GameObject player;
    public float speed;//设置速度
    private Rigidbody2D rb;
    private Vector2 playerStartPosit;
    private Vector2 trunkStartPosit;
    public float activeTime;
    // Start is called before the first frame update
    void Start()
    {
        activeTime = 3.0f;
        trunkPosit = GameObject.FindGameObjectWithTag("Trunk");
        trunkStartPosit=trunkPosit.GetComponent<Transform>().position;//获取小怪在子弹发射时的初始位置
        player = GameObject.FindGameObjectWithTag("Player");
        playerStartPosit = player.transform.position;//获取玩家在子弹发射时的初始位置
        rb = GetComponent<Rigidbody2D>();
        speed = 12;//初始化速度
    }

    // Update is called once per frame
    void Update()
    {
        islaunch();
        activeTimeOver();//时间到后失活
    }
    private void activeTimeOver()
    {
        activeTime -= Time.deltaTime;
        if(activeTime<=0)
        {
            Destroy(this.gameObject);//失活
        }
    }
    private void islaunch()
    {
        if(playerStartPosit.x < trunkStartPosit.x)//玩家在左
        {
            rb.velocity = Vector2.left * speed;
        }
        else//玩家在右边
        {
            rb.velocity = Vector2.right * speed;
        }
          /*  rb.velocity = new Vector2(speed * (playerStartPosit.x - firePoint.GetComponent<Transform>().position.x), rb.velocity.y);*///移向玩家的方向
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision. gameObject.CompareTag("Player"))//碰到玩家也会使子弹消失
        {
           Destroy(this.gameObject);//失活
        }
    }
}

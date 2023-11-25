using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinInEnemy : MonoBehaviour//小怪金币掉落实现
{
    public Turtle turtle1;
    public Trunk trunk1;
    private Rigidbody2D rb;
    public float springFouce;//弹起力
    public float fouceTime;//弹起时间
    bool isactive;    //判断是否激活
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void isEnemyDie()
    {
        
        if (turtle1.health <= 0)//爆金币咯
        {
           fouceTime -= Time.deltaTime;//时间减少
            if (fouceTime >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, springFouce);//弹起
            }
        }
        if (turtle1.health <= 0)//爆金币咯
        {
            fouceTime -= Time.deltaTime;//时间减少
            if (fouceTime >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, springFouce);//弹起
            }
        }


    }
}

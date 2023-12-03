using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coinInEnemy : MonoBehaviour//小怪金币掉落实现
{
    private Rigidbody2D rb;
    public float springSpeed;//弹起力
    public float springTime;//弹起时间
    bool isactive;    //判断是否激活
    private GameObject player;
    public float speed;//金币移动向玩家的速度
    public float staticTime;//静止一段时间再移向玩家
    private Collider2D coinColli;
    // Start is called before the first frame update
    void Start()
    {
        springTime = 0.5f;
        springSpeed = 3.0f;
        coinColli = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        springTime -= Time.deltaTime;
        if(staticTime<=0)
        {
            coinColli.forceReceiveLayers =LayerMask.NameToLayer("Nothing") ;
        //移向玩家
          transform.position = Vector3.MoveTowards(this.transform.position, player.GetComponent<Transform>().position, speed * Time.deltaTime);
        }
        else//金币移向玩家之前
        {
            staticTime -= Time.deltaTime;
            if(springTime>=0)
            {
            coinBounce();
            } 
        }

    }
    private void coinBounce()//金币掉落弹起功能
    {
       rb.velocity = new Vector2(rb.velocity.x, springSpeed);//弹起
    }
}

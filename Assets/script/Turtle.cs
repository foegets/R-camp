using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Turtle : Enemy
{
    public float speed = 3;//速度
    public float waitTime;//总时间
    public Transform[] position;//两点设置
    private float wait;//用于更新的时间 
    private bool isright;//方向
    private int i = 0;//改变两点的参数
    private Animator animator;
    public float attackTime;//攻击动画播放时间
    bool isPlayHitAnimat;
    public Transform playerTransform;
    public Animator repelEffectAnim;
    private string direction;
    private GameObject Player;
    public GameObject coin;//金币预制体对象导入
    private bool isDie;//
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 0.5f;
        Player = GameObject.FindGameObjectWithTag ("Player");
        wait = waitTime;//更新时间       
        animator = GetComponent<Animator>();
        health = 30;//更新小怪数值
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        traverse();//小怪来回移动
        print("小怪生命" + health);
        TurtleDie();//小怪死亡后爆金币
        allTimeCoundDown();//倒计时所有需要计时的time
    }
    private void allTimeCoundDown()
    {
        damageTime -= Time.deltaTime;//小怪受伤后时间倒计时

    }
    private void TurtleDie()//小怪死亡
    {
        if(health<=0)
        {
            isDie = true;//确定小怪已经死亡，防止在碰撞检测中生成两个或以上的金币预制体
            StartCoroutine(turtleDeathAnimation());
        }
    }
    IEnumerator turtleDeathAnimation()
    {
        animator.Play("turtle die");
        yield return new WaitForSeconds(0.24f);
        gameObject.SetActive(false);
    }
    private void traverse()//小怪来回移动
    {
        transform.position = Vector2.MoveTowards(this.transform.position, position[i].position, speed * Time.deltaTime);//移向此点
        //控制位移
        if (Vector2.Distance(transform.position, position[i].position) <= 0.1f)
        {
           
            if(wait<=0)
            {
                if (i == 0)//更新i
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                if (isright == false)//left
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);//转向
                    direction = "left";
                    isright = true;
                }
                else//right
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);//转向
                    direction = "right";
                    isright = false;
                } 
                wait = waitTime;//更新时间
            }
            else
            {
                wait -= Time.deltaTime;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.gameObject.CompareTag("Player"))//小怪普攻触发
        {
            player.isContactEnemy = true;
           StartCoroutine(CheckHitAnimation());
        }
        if(collision.gameObject.CompareTag ("playerattack") && damageTime<=0)
        {
            health -= Player.GetComponent<player>().playerDamage;//减少小怪血量
            bloodEffect();//流血特效
            animator.SetTrigger("isInjury");//小怪受伤动画
            damageTime = 0.3f;//重置小怪受伤无敌时间
            if(direction=="right")//击退特效的实现
            {
                if(transform.position.x-playerTransform.position.x>=0)//(玩家在左）
                {
                  repelEffectAnim.SetTrigger("isattack2"); 

                }
                  else//玩家在右
                  {
                     repelEffectAnim.SetTrigger("isattack1");                
                } 
            }
            else if ((direction=="left"))
            {
                if (transform.position.x - playerTransform.position.x >= 0)//(玩家在右）
                {
                    repelEffectAnim.SetTrigger("isattack1");
                }
                else//玩家在右
                {
                    repelEffectAnim.SetTrigger("isattack2");
                }
            }

            if (collision.gameObject.CompareTag("Player"))//(攻击玩家）
            {
                StartCoroutine(CheckHitAnimation());//小怪普攻触发
            }
            if (collision.gameObject.CompareTag("playerattack") && (health - damageTime) <= 0&& !isDie)//检测是不是最后一击
            {
                 Instantiate(coin,transform.position,Quaternion.identity);//产生金币预制体
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)//碰撞检测
    {
       

    }
    IEnumerator CheckHitAnimation()//小怪普通攻击协程
        {
            animator.Play("Turtle Hit");//播放攻击动画
            yield return new WaitForSeconds(attackTime);
        }
}
    
    


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Trunk : Enemy
{
    public Transform firePointTrans;
    public static bool isBulletTouchPlayer;
    public float damageBullet;//子弹伤害
    public Transform playerTrans;//为获取玩家位置等信息
    public Rigidbody2D playRg;//控制玩家受击后被击退
    public float repelFouce;//击退力
    public Transform position1;//两点限制小怪位移范围
    public Transform position2;
    public float speed;//移动速度 
    public bool canHit;//传输信息用的bool值
    public   Animator animator;
    public float cd;//子弹冷却时间
    public Transform playerTransform;
    private GameObject repelEffect;//受伤特效的动画引入
    public GameObject Player;//为了导入玩家脚
    public GameObject coin;//金币游戏预制体导入
    public GameObject bullet;//子弹预制体导入
    private bool isdie;
    private Vector2 startScale;
    // Start is called before the first frame update
      void Start()
    {
        repelEffect = GameObject.FindGameObjectWithTag("repelEffect");
        startScale = transform.localScale;
        Player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        maxHp = 20;
        health = maxHp;//初始化数值
        speed = 5;
        damageBullet = 5.0f;
        cd = 1.70f;
    }

    // Update is called once per frame
    void Update()
    {
        isDie();//判断死亡并失活
        moveToward();//移向玩家
        isHit();//攻击
        damageTime -= Time.deltaTime;
        allTimeCoundDown();//倒计时所有需要计时的time
    }
    private void allTimeCoundDown()
    {
        damageTime -= Time.deltaTime;//小怪受伤后时间倒计时
    }
    private void isHit()
    {
        if (playerTrans.transform.position.y >= position1.position.y - 0.3f
           && playerTrans.transform.position.y <= position1.position.y + 0.3f)
        {
            animator.SetBool("isHit", true);//攻击动画
        }
        else
        {
            animator.SetBool("isHit", false);
        }
    }
    public void activateBullet()
    {//在指定位置激活预制体
             Instantiate(bullet, firePointTrans.position, Quaternion.identity);
    }
    private void moveToward()//移向玩家
    {
        //判断玩家位置是否在范围内
        if(playerTrans.position.x>=position2.position.x 
            && playerTrans.position.x <= position1.position.x 
            && playerTrans.position.y>=position1.position.y-0.7f 
            && playerTrans.position.y<=position1.position.y+15.0f)
        {
            //移向玩家
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerTrans.position.x,this.transform.position.y), speed * Time.deltaTime);
            if(playerTrans.position.x-this.transform.position.x>=0)//方向转换实现
            {
                transform.localScale = new Vector2(-startScale.x, startScale.y);//右边
            }
            else if(playerTrans.position.x - this.transform.position.x < 0)
            {
                transform.localScale = startScale;//左边       
            }
        }
    }
    private void isDie()
    {
        if(health<=0)
        {
            isdie = true;//确定小怪已经死亡，防止在碰撞检测中生成两个或以上的金币预制体
            animator.Play("trunk die");//播放动画
            StartCoroutine(trunkDie());//死亡延迟携程   (也可以通过动画里加事件来解决）
        }
    }
    IEnumerator trunkDie()
    {
        yield return new WaitForSeconds(0.5f);
        speed = 0;
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if(collision.gameObject.CompareTag("playerattack") && damageTime<=0)
        {
            health -= Player.GetComponent<player>().playerDamage;//减少生命
            bloodEffect();//流血粒子
            animator.SetTrigger("isInjury");//小怪受伤动画
            damageTime = 0.3f;//重置时间
            if (transform.position.x - playerTransform.position.x <= 0)//击退特效(玩家在左）
            {
                repelEffect.GetComponent<Animator>().SetTrigger("isattack1");
            }
            else//玩家在右
            {
                repelEffect.GetComponent<Animator>().SetTrigger("isattack2");
            }
            if(collision.gameObject.CompareTag("playerattack") && (health-damageTime)<=0 && !isdie)//检测是不是最后一击
            {
                Instantiate(coin, transform.position, Quaternion.identity);//产生金币预制体
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测
    {
        //if (collision.gameObject.CompareTag("Player"))//使玩家被击退
        //{
        //    if (playerTrans.position.x - this.transform.position.x >= 0)
        //    {
        //        playRg.velocity = new Vector2(repelFouce, playRg.velocity.y);
        //    }
        //    else if (playerTrans.position.x - this.transform.position.x < 0)
        //    {
        //        playRg.velocity = new Vector2(repelFouce, playRg.velocity.y);
        //    }
        //}
    
    }
    private void OnCollisionStay2D(Collision2D collision)//碰撞检测
    {
        //if (collision.gameObject.CompareTag("Player"))//使玩家被击退
        //{
        //    if (playerTrans.position.x - this.transform.position.x >= 0)
        //    {
        //        playRg.velocity = new Vector2(repelFouce, playRg.velocity.y);
        //        print("daljgadjkhglkadhgkjdahsg");
        //    }
        //    else if (playerTrans.position.x - this.transform.position.x < 0)
        //    {
        //        playRg.velocity = new Vector2(repelFouce, playRg.velocity.y);
        //    }
        //}   
    }
}

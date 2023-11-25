using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Trunk : Enemy
{
    public static bool isBulletTouchPlayer;
    public float damageBullet;//子弹伤害
    public Transform playerTrans;//为获取玩家位置等信息
    public Rigidbody2D playRg;//控制玩家受击后被击退
    public float repelFouce;//击退力
    public Transform position1;//两点限制小怪位移范围
    public Transform position2;
    public float speed;//移动速度
    private SpriteRenderer spriteRenderer;
    public bool canHit;//传输信息用的bool值
    public   Animator animator;
    public Rigidbody2D bukketRigidbody2D;
public GameObject bullet;//获取子类子弹
    public float cd;//子弹冷却时间
    //导入三个金币的脚本
    public coinInEnemy coinInTrunk1;
    public coinInEnemy coinInTrunk2;
    public coinInEnemy coinInTrunk3;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        maxHp = 20;
        health = maxHp;//初始化数值
        speed = 5;
        damageBullet = 5.0f;
        bullet.isStatic = false;//初始化bool值（子弹）
        //print(position1.position.x);
        cd = 1.70f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        isDie();//判断死亡并失活
        moveToward();//移向玩家
        isHit();//攻击
        damageTime -= Time.deltaTime;
    }
    private void isHit()
    {
         cd -= Time.deltaTime;
        if (playerTrans.transform.position.y >= position1.position.y - 0.1f
           && playerTrans.transform.position.y <= position1.position.y + 0.1f
           /* &&*/ /*bullet.GetComponent<trunkBullet>().isactive==false*/ )
        {
            if(bullet.GetComponent<trunkBullet>().isactive == false && cd<=0||isBulletTouchPlayer==true)
            {
             //激活子类
                bullet.SetActive(true);            
                canHit = true;
                //重置位置
                bullet.transform.position = this.transform.position;
                if (spriteRenderer.flipX == true)//向右发射
                {
                  bukketRigidbody2D .velocity = Vector2.right * speed;
                }
                else //向左发射
                {
                   bukketRigidbody2D .velocity = Vector2.left * speed;
                }
                cd = 1.70f;
                isBulletTouchPlayer = false;
            } 
        }

    }
    private void moveToward()//移向玩家
    {
        //判断玩家位置是否在范围内
        if(playerTrans.position.x>=position2.position.x 
            && playerTrans.position.x <= position1.position.x 
            && playerTrans.position.y>=position1.position.y-0.1f 
            && playerTrans.position.y<=position1.position.y+15)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerTrans.position.x,this.transform.position.y), speed * Time.deltaTime);//移向玩家
            if(playerTrans.position.x-this.transform.position.x>=0)//方向转换实现
            {
                spriteRenderer.flipX = true;//右边
            }
            else if(playerTrans.position.x - this.transform.position.x < 0)
            {
                spriteRenderer.flipX = false;//左边       
            }
        }
    }
    private void isDie()
    {
        if(health<=0)
        {
            coinInTrunk1.gameObject.SetActive(true);//激活金币对象
            coinInTrunk2.gameObject.SetActive(true);
            coinInTrunk3.gameObject.SetActive(true);
            coinInTrunk1.gameObject.transform.position = this.transform.position;//重置位置
            coinInTrunk2.gameObject.transform.position = this.transform.position;
            coinInTrunk3.gameObject.transform.position = this.transform.position;
            animator.Play("trunk die");//播放动画
            StartCoroutine(trunkDie());//死亡延迟携程   
        }
    }
    IEnumerator trunkDie()
    {
        yield return new WaitForSeconds(0.5f);
        speed = 0;
        this.gameObject.SetActive(false);
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
        if(collision.gameObject.CompareTag("playerattack"))
        {
            print("成功");
            bloodEffect();
            Invoke("takeDamage", 0.16f);//使玩家动画完再改变bool值
            print("66");
        }
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
    public override void disReDamage()
    {
        base.disReDamage();
    }
}

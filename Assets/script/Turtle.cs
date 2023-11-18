using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    public coinInEnemy coinInTurtle1;
    public coinInEnemy coinInTurtle2;
    public float speed = 3;//速度
    public float waitTime;//总时间
    public Transform[] position;//两点设置
    private float wait;//用于更新的时间
    private bool isright;//方向
    private int i = 0;//改变两点的参数
    private Animator animator;
    public float attackTime;//攻击动画播放时间
    bool isPlayHitAnimat;
    // Start is called before the first frame update
    void Start()
    {
        //coinInTurtle = GetComponentInChildren<coinInTurtle>();
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
    }
    private void TurtleDie()//小怪死亡
    {
        if(health<=0)
        {
             
            coinInTurtle1.gameObject.SetActive(true);//激活金币对象
            coinInTurtle2.gameObject.SetActive(true);
            coinInTurtle1.gameObject.transform.position = this.transform.position;//重置位置
            coinInTurtle2.gameObject.transform.position = this.transform.position;
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
        //控制位移
        if (Vector2.Distance(transform.position, position[i].position) == 0)
        {
            if (wait > 0)
            {
                wait -= Time.deltaTime;//减少时间
            }
            else
            {
                if (i == 0)//更新i
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                if (isright == false)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);//转向
                    isright = true;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);//转向
                    isright = false;
                }
                wait = waitTime;//更新时间
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, position[i].position, speed * Time.deltaTime);//移向此点
            print("1");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isContactEnemy = true;
           StartCoroutine(CheckHitAnimation());//小怪普攻触发
        }
        //if(collision.gameObject.CompareTag("playerattack"))
        //{
        //    animator.SetTrigger("isInjury");
        //    print("dfaff");
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)//碰撞检测
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isContactEnemy = true;
            StartCoroutine(CheckHitAnimation());//小怪普攻触发
        }
        //if(collision.gameObject.CompareTag("playerattack"))
        //{
        //    animator.SetTrigger("isInjury");
        //    print("dfaff");
        //}
    }
    IEnumerator CheckHitAnimation()//小怪普通攻击协程
    {       
            animator.Play("Turtle Hit");//播放攻击动画
            yield return new WaitForSeconds(attackTime);       
    }
}

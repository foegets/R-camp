using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private bool isattack=false;
    private int combostep=0;//攻击段数
    private float timer;//计时器
    private float interval=2f;//可以连续攻击的时间间隔
    public static float direction;//移动方向
   public static int jumpingNum;//可以跳跃的次数
    Animator animator;
    private Rigidbody2D rb;
    public float speed = 10.0f;//速度
    public static bool onground;//是否在地面参数
    public float jumpfouce = 10.0f;//跳跃参数
    public float superRunFouce = 20.0f;//冲刺参数
    int airDash;//空中冲刺参数
    private  Coroutine checkerAttack;
    public static bool isContactEnemy;//是否接触怪物
    public  float playerDamage;//玩家伤害
    public  static float playerHealth;//玩家生命
    public static float maxPlayerHp;
    public GameObject obj1;//在外部拖入Turtle
    private float canInjuryTime;//受伤无敌的冷却时间
    private AudioSource attackSound;
    public bool canAttack;//是否可以攻击（防止受伤时可以攻击）
    private Vector3 playerScale;
    private GameObject trunk;
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
        maxPlayerHp = 60;//初始化生命值
         playerHealth=maxPlayerHp ;
        playerDamage = 6;
        canInjuryTime = 1;
        playerScale = transform.localScale;
        canAttack = true;
        trunk = GameObject.FindGameObjectWithTag("Trunk");
    }
    // Update is called once per frame
    void Update()
    { 
       direction = Input.GetAxis("Horizontal");
        playerMove(direction);//角色移动实现
        playerjumping();//角色跳跃
        dash(direction);//冲刺实现       
        playerattack();//角色普通攻击
        playdeath();
        resetInjuryTime();//刷新受伤后无敌的时间
    }  

    private void playerattack()//角色普攻
    {
        if(Input.GetMouseButtonDown(0) && !isattack && canAttack)//不加isattack的话会导致普攻不间断
                                                                 //canAttack防止小怪在玩家受伤时还可以攻击
        {
            isattack = true;
            combostep++;
            timer = interval;//表示重新开始计时（需要重新开始计时时将time再次初始化
            if(combostep>3)
            {
                combostep = 1;
            }
            animator.SetTrigger("isattack");
            animator.SetInteger("combostep", combostep);
            attackSound.Play();
        }
        if(timer>=0)
        {
            timer -= Time.deltaTime;
            
        }
       else if(timer<=0)//连击断了
            {
                combostep = 0;//初始化数值
                //timer = 0;
            }
    }
    public void AttackOver()
    {
        isattack = false;
    }
  private void playerjumping()//角色跳跃
    {
 if ( Input.GetKeyDown(KeyCode.Space)&&jumpingNum>0) //跳跃实现//如果增加onground==true作为条件就会导致第二段跳起不来
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpfouce);
            onground = false;
            jumpingNum--;
        }

        //跳跃动画
        if (onground == false)
        {
            animator.SetBool("isjumping", true);
        }
        else if(onground==true)
        {
          animator.SetBool("isjumping", false);
        }
    }
   private void playerMove(float direction)//角色移动
    {
 //rb.AddForce(new Vector2(direction * speed, rb.velocity.y));
          rb.velocity = new Vector2(direction * speed, rb.velocity.y);
          if (onground == true && direction != 0) //移动动画
          {

            animator.SetBool("isrunning", true);
          }
          else if (onground == false || direction == 0)
          {
            animator.SetBool("isrunning", false);
          }
          //移动左右方向转换
          if (direction > 0)//不使用localscale而使用flipx的话，会导致后面子类普攻碰撞盒无法随玩家作用移动而改变
          {
            transform.localScale = playerScale;
          }
          else if (direction < 0)
          {
              Vector3 playerScale1 = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
              transform.localScale = playerScale1;
          }
        
    }
 private void dash(float direction)//冲刺实现
{
    if (Input.GetKey(KeyCode.LeftShift) && direction != 0)
    {
        if (onground == true)//在地面冲刺
        {
            if (direction >= 0)//向右
            {
                rb.velocity = new Vector2(superRunFouce , this.rb.velocity.y);
                animator.SetTrigger("issuperrun");//冲刺动画
            }
            else//向左
            {
                rb.velocity = new Vector2(-superRunFouce , this.rb.velocity.y);
                animator.SetTrigger("issuperrun");//冲刺动画
            }
        }
        else if (/*airDash > 0 &&*/ !onground && direction >= 0)//在空中冲刺向右
        {
            rb.velocity = new Vector2(superRunFouce* 1.5f, this.rb.velocity.y);
            animator.SetTrigger("issuperrun");//冲刺动画
                //airDash--;                   
        }
        else if (/*airDash > 0 &&*/ !onground && direction <= 0)//空中冲刺向左
        {
            rb.velocity = new Vector2(-superRunFouce  * 1.5f, this.rb.velocity.y);
            animator.SetTrigger("issuperrun");//冲刺动画
                //airDash--;                                  

        }
    }
}
    private void OnTriggerEnter2D(Collider2D other)  //子弹和平台碰撞检测
    {
        if (other.gameObject.CompareTag("platform"))
        {
            onground = true;
            jumpingNum = 2;
        }
        if(canInjuryTime<=0)
        {
          if (other.gameObject.CompareTag("Trunk Bullet"))//树怪子弹
           {
            obj1 = other.gameObject;
            playerHealth -= trunk.GetComponent<Trunk>().damageBullet; 
            animator.SetTrigger("isPlayerInjury");//受伤动画
                canInjuryTime = 1;
                canAttack=false;
                Invoke("setcanAttack", 0.26f);
            }
        }
     
    } 
    void setcanAttack()
        {
            canAttack = true;
        }
    private void OnCollisionStay2D(Collision2D collision)  //小怪持续碰撞检测
    {
        
        if (canInjuryTime <= 0)//玩家受伤后的无敌时间
        {
            if (collision.gameObject.CompareTag("Turtle"))
           {
            obj1 = collision.gameObject;
            playerHealth -= obj1.GetComponent<Turtle>().damage;//减少生命
            animator.SetTrigger("isPlayerInjury");//受伤动画
            canInjuryTime = 1;//一般在发挥效果的地方重置时间变量
                canAttack = false;
                Invoke("setcanAttack", 0.26f);
            }
            if(collision.gameObject.CompareTag("Trunk"))
            {
                obj1 = collision.gameObject;
                playerHealth -= obj1.GetComponent<Trunk>().damage;//减少生命
                animator.SetTrigger("isPlayerInjury");//受伤动画
                canInjuryTime = 1;//一般在发挥效果的地方重置时间变量
                canAttack = false;
                Invoke("setcanAttack", 0.26f);
            }
        }
    }
    private void resetInjuryTime()//单独把无敌时间写成函数而不是写在碰撞检测里是
                                  //为了不让碰撞时才让无敌时间减少
    {
        canInjuryTime -= Time.deltaTime;
    }
    private void playdeath()//玩家死亡
    {
        if(playerHealth<=0)
        {
           StartCoroutine(playerDeathAnimation());//也可以在动画里插入事件来解决
            speed = 0;//重置数值
            jumpfouce = 0;
        }
    }
    IEnumerator playerDeathAnimation()
    {
        animator.Play("player death");
        yield return new WaitForSeconds(1.04f);
        this.gameObject.SetActive(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public static float direction;//移动方向
   public static int jumpingNum;//可以跳跃的次数
    SpriteRenderer spriteRenderer;
    Animator animator;
    private Rigidbody2D rb;
    public float speed = 10.0f;//速度
    public static bool onground;//是否在地面参数
    public float jumpfouce = 10.0f;//跳跃参数
    public float superRunFouce = 20.0f;//冲刺参数
    int airDash;//空中冲刺参数
    private  Coroutine checkerAttack;
    public static int coinNum;
    public static bool isContactEnemy;//是否接触怪物
    public static float playerDamage;//玩家伤害
    public  static float playerHealth;//玩家生命
    public GameObject obj1;//在外部拖入Turtle
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = 60;//初始化生命值
        playerDamage = 6;
    }

    // Update is called once per frame
    void Update()
    { 
       direction = Input.GetAxis("Horizontal");
        playerMove(direction);//角色移动实现
        playerjumping();//角色跳跃
        dash(direction);//冲刺实现       
        playerattack();//角色普通攻击
        /* isIdle(direction);*///判断是否为Idle
        checkerAttack = StartCoroutine(CheckAttackAnimation());//普通攻击协程
        playdeath();
    }  
    IEnumerator CheckAttackAnimation()//普通攻击协程
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("player attack");
            yield return null;
        }
    }
    private void playerattack()//角色普攻
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isattack");
            soundManager.playerattack();//播放音乐
        }
    }
    //private void isIdle(float direction)//判断是否Idle
    //{
    //    if(onground==true && direction==0)
    //    {
    //        animator.SetBool("isIdle",true);
    //    }
    //}
  private void playerjumping()//角色跳跃
    {
 if ( Input.GetKeyDown(KeyCode.Space)&&jumpingNum>0) //跳跃实现
        {
            //rb.AddForce(new Vector2(rb.velocity.x, jumpfouce));
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
        if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = true;
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
    private void OnTriggerEnter2D(Collider2D other)  //碰撞检测
    {
        if (other.gameObject.CompareTag("platform"))
        {
            onground = true; 
            jumpingNum =2 ;
            //airDash = 1;
        }
        if(other.gameObject.CompareTag("Turtle"))
        {
            obj1 = other.gameObject;
            playerHealth -=  obj1.GetComponent<Turtle>().damage;//减少生命
            animator.SetTrigger("isInjury");//受伤动画
        }
        if (other.gameObject.CompareTag("Trunk"))//树怪
        {
            obj1 = other.gameObject;
            playerHealth -= obj1.GetComponent<Trunk>().damage;//减少生命
            animator.SetTrigger("isInjury");//受伤动画
        }
        if (other.gameObject.CompareTag("Trunk Bullet"))//树怪子弹
        {
            obj1 = other.gameObject;
            playerHealth -= 4; /*(obj1.GetComponent<Trunk>().damage)*2;*/
            animator.SetTrigger("isInjury");//受伤动画
        }
    }
    //private void OnTriggerStay2D(Collider2D other)  //持续碰撞检测
    //{
    //    if (other.gameObject.CompareTag("platform"))
    //    {
    //        onground = true;
    //        jumpingNum = 2;
    //        //airDash = 1;
    //    }
    //    if (other.gameObject.CompareTag("Turtle"))
    //    {
    //        obj1 = other.gameObject;
    //        playerHealth -= obj1.GetComponent<Turtle>().damage;//减少生命
    //        animator.SetTrigger("isInjury");//受伤动画
    //    }
    //    if (other.gameObject.CompareTag("Trunk"))//树怪
    //    {
    //        obj1 = other.gameObject;
    //        playerHealth -= obj1.GetComponent<Trunk>().damage;//减少生命
    //        animator.SetTrigger("isInjury");//受伤动画
    //    }
    //    if (other.gameObject.CompareTag("Trunk Bullet"))//树怪子弹
    //    {
    //        obj1 = other.gameObject;
    //        playerHealth -= 4; /*(obj1.GetComponent<Trunk>().damage)*2;*/
    //        animator.SetTrigger("isInjury");//受伤动画
    //    }
    //}
    private void playdeath()//玩家死亡
    {
        if(playerHealth<=0)
        {
           StartCoroutine(playerDeathAnimation());
            speed = 0;//重置数值
            jumpfouce = 0;
        }
    }
    IEnumerator playerDeathAnimation()
    {
        animator.Play("player death");
        yield return new WaitForSeconds(0.29f);
        this.gameObject.SetActive(false);
    }
    
}

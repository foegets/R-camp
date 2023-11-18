using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //所有的组件获取(用protected保护组件，只能被子类访问，不能被character啊这些访问)
    //刚体（敌人）
    protected Rigidbody2D rb;
    //动画机
    protected Animator anim;
    //物理检测代码
    PhysicsCheck physicsCheck;

    [Header("基本参数")]
    //普通移动速度
    public float normalSpeed;
    //识别到玩家后的追击速度
    public float chaseSpeed;
    //当前速度
    public float currentSpeed;
    //当前运动方向
    public Vector3 faceDir;
    //受伤所受的力的大小
    public float hurtForce;

    //记录攻击者
    public Transform attacker;

    [Header("计时器")]
    //撞墙之后的等待时间~
    public float waitTime;
    //等待时间计时器
    public float waitTimeCounter;
    //等待状态
    public bool wait;

    [Header("状态")]
    //受伤
    public bool isHurt;
    //死亡
    public bool isDead;

    //一开始就要完成的任务！
    private void Awake()
    {
        //获取组件使用权
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //目前速度等于普通移动速度
        currentSpeed = normalSpeed;
        //是撞墙等待计时器时间为默认值
        waitTimeCounter = waitTime;
    }

    //持续不断的进行操作！
    private void Update()
    {
        //面朝方向（大于0 是往左走？？）
        faceDir = new Vector3(-transform.localScale.x,0,0);
        //如果撞墙就转身捏
        if((physicsCheck.touchLeftWall)&&(faceDir.x < 0) || (physicsCheck.touchRightWall)&&(faceDir.x > 0))
        {   
            //进入等待状态
            wait = true;
            //播放idle动画
            anim.SetBool("walk",false);
            //等待倒计时结束后转身
            TimeCounter();

        }

        
    }

    //固定频率
    private void FixedUpdate()
    {
        if(!isHurt & !isDead)
            Move();
    }

    //创建移动的函数方法(virtual修饰符，表虚拟，不固定，子类可通过访问该函数进行修改)
    public virtual void Move()
    {
        //给野猪boar施加一个速度，x轴上为当前速度乘面朝方向乘时间修正，y轴上为刚体原来在y轴上的速度（切忌不能直接等于0！）
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime,rb.velocity.y);
    }

    //所有和计时有关的都可以写进来
    public void TimeCounter()
    {
        if(wait)
        {
            //撞墙等待时间递减
            waitTimeCounter -= Time.deltaTime;
            //当撞墙等待时间过完之后，取消等待状态
            if(waitTimeCounter <= 0){
                //取消等待
                wait = false;
                //计时器恢复为初始值
                waitTimeCounter = waitTime;
                //转身
                transform.localScale = new Vector3(faceDir.x,1,1);
                //恢复走路动画
                anim.SetBool("walk",true);
            }
        }
    }

    //受伤相关(括号里是发起攻击的对象，即玩家)
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //受击转身
        if(attackTrans.position.x - transform.position.x>0)
            transform.localScale = new Vector3(-1,1,1);
        if(attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1,1,1);

        //受伤被击退,播放击退动画
        isHurt = true;
        anim.SetTrigger("hurt");
        //创建一个变量来记录攻击的方向
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x,0).normalized;
        
        //开始携程
        StartCoroutine(OnHurt(dir));
    }

    //创建一个返回迭代器的函数方法（受击）
    private IEnumerator OnHurt(Vector2 dir)
    {
        //施加受击的力，达到击退效果
        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
        //等待
        yield return new WaitForSeconds(0.45f);
        //等待一段时间后取消击退状态
        isHurt = false;
    }

    //死亡相关
    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead",true);
        isDead = true;
    }

    //动画结束之后进行销毁
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public static float direction;//�ƶ�����
   public static int jumpingNum;//������Ծ�Ĵ���
    SpriteRenderer spriteRenderer;
    Animator animator;
    private Rigidbody2D rb;
    public float speed = 10.0f;//�ٶ�
    public static bool onground;//�Ƿ��ڵ������
    public float jumpfouce = 10.0f;//��Ծ����
    public float superRunFouce = 20.0f;//��̲���
    int airDash;//���г�̲���
    private  Coroutine checkerAttack;
    public static int coinNum;
    public static bool isContactEnemy;//�Ƿ�Ӵ�����
    public static float playerDamage;//����˺�
    public  static float playerHealth;//�������
    public static float maxPlayerHp;
    public GameObject obj1;//���ⲿ����Turtle
    private float canInjuryTime;//�����޵е���ȴʱ��
    private AudioSource attackSound;
    public bool canAttack;//�Ƿ���Թ�������ֹ����ʱ���Թ�����
    private Vector3 playerScale;
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackSound = GetComponent<AudioSource>();
        maxPlayerHp = 60;//��ʼ������ֵ
         playerHealth=maxPlayerHp ;
        playerDamage = 6;
        canInjuryTime = 1;
        playerScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    { 
       direction = Input.GetAxis("Horizontal");
        playerMove(direction);//��ɫ�ƶ�ʵ��
        playerjumping();//��ɫ��Ծ
        dash(direction);//���ʵ��       
        playerattack();//��ɫ��ͨ����
        /* isIdle(direction);*///�ж��Ƿ�ΪIdle
        checkerAttack = StartCoroutine(CheckAttackAnimation());//��ͨ����Э��
        playdeath();
        resetInjuryTime();//ˢ�����˺��޵е�ʱ��
    }  
    IEnumerator CheckAttackAnimation()//��ͨ����Э��
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("player attack");
            yield return null;
        }
    }
    private void playerattack()//��ɫ�չ�
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isattack");
            attackSound.Play();
        }
    }
  private void playerjumping()//��ɫ��Ծ
    {
 if ( Input.GetKeyDown(KeyCode.Space)&&jumpingNum>0) //��Ծʵ��
        {
            //rb.AddForce(new Vector2(rb.velocity.x, jumpfouce));
            rb.velocity = new Vector2(rb.velocity.x, jumpfouce);       
            onground = false;
            jumpingNum--;
        }

        //��Ծ����
        if (onground == false)
        {
            animator.SetBool("isjumping", true);
        }
        else if(onground==true)
        {
          animator.SetBool("isjumping", false);
        }
    }
   private void playerMove(float direction)//��ɫ�ƶ�
    {
        //rb.AddForce(new Vector2(direction * speed, rb.velocity.y));
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        if (onground == true && direction != 0) //�ƶ�����
        {

            animator.SetBool("isrunning", true);
        }
        else if (onground == false || direction == 0)
        {
            animator.SetBool("isrunning", false);
        }
        //�ƶ����ҷ���ת��
        if (direction > 0)
        {
            //spriteRenderer.flipX = false;
            transform.localScale = playerScale;
        }
        else if (direction < 0)
        {
            Vector3 playerScale1 = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
            transform.localScale = playerScale1;
        }
    }
 private void dash(float direction)//���ʵ��
{
    if (Input.GetKey(KeyCode.LeftShift) && direction != 0)
    {
        if (onground == true)//�ڵ�����
        {
            if (direction >= 0)//����
            {
                rb.velocity = new Vector2(superRunFouce , this.rb.velocity.y);
                animator.SetTrigger("issuperrun");//��̶���
            }
            else//����
            {
                rb.velocity = new Vector2(-superRunFouce , this.rb.velocity.y);
                animator.SetTrigger("issuperrun");//��̶���
            }
        }
        else if (/*airDash > 0 &&*/ !onground && direction >= 0)//�ڿ��г������
        {
            rb.velocity = new Vector2(superRunFouce* 1.5f, this.rb.velocity.y);
            animator.SetTrigger("issuperrun");//��̶���
                //airDash--;                   
        }
        else if (/*airDash > 0 &&*/ !onground && direction <= 0)//���г������
        {
            rb.velocity = new Vector2(-superRunFouce  * 1.5f, this.rb.velocity.y);
            animator.SetTrigger("issuperrun");//��̶���
                //airDash--;                                  

        }
    }
}
    private void OnTriggerEnter2D(Collider2D other)  //��ײ���
    {
        if(canInjuryTime<=0)
        {
          if (other.gameObject.CompareTag("Trunk Bullet"))//�����ӵ�
           {
            obj1 = other.gameObject;
            playerHealth -= 4; /*(obj1.GetComponent<Trunk>().damage)*2;*/
            animator.SetTrigger("isInjury");//���˶���
                canInjuryTime = 1;
                canAttack = true;
                Invoke("setcanAttack", 0.5f);
            }
        }
     
    } 
    void setcanAttack()
        {
            canAttack = false;
        }
    private void OnCollisionStay2D(Collision2D collision)  //������ײ���
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            onground = true;
            jumpingNum = 2;
            //airDash = 1;
        }
        if (canInjuryTime <= 0)
        {
            if (collision.gameObject.CompareTag("Turtle"))
           {
            obj1 = collision.gameObject;
            playerHealth -= obj1.GetComponent<Turtle>().damage;//��������
            animator.SetTrigger("isPlayerInjury");//���˶���
            canInjuryTime = 1;
                canAttack = true;
                Invoke("setcanAttack", 0.26f);
            }
           if (collision.gameObject.CompareTag("Trunk"))//����
           {
            obj1 = collision.gameObject;
            playerHealth -= obj1.GetComponent<Trunk>().damage;//��������
            animator.SetTrigger("isPlayerInjury");//���˶���
            canInjuryTime = 1;
                canAttack = true;
                Invoke("setcanAttack", 0.26f);
            }
        }
    }
    private void resetInjuryTime()
    {
        canInjuryTime -= Time.deltaTime;
    }
    private void playdeath()//�������
    {
        if(playerHealth<=0)
        {
           StartCoroutine(playerDeathAnimation());
            speed = 0;//������ֵ
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    private bool isattack=false;
    private int combostep=0;//��������
    private float timer;//��ʱ��
    private float interval=2f;//��������������ʱ����
    public static float direction;//�ƶ�����
   public static int jumpingNum;//������Ծ�Ĵ���
    Animator animator;
    private Rigidbody2D rb;
    public float speed = 10.0f;//�ٶ�
    public static bool onground;//�Ƿ��ڵ������
    public float jumpfouce = 10.0f;//��Ծ����
    public float superRunFouce = 20.0f;//��̲���
    int airDash;//���г�̲���
    private  Coroutine checkerAttack;
    public static bool isContactEnemy;//�Ƿ�Ӵ�����
    public  float playerDamage;//����˺�
    public  static float playerHealth;//�������
    public static float maxPlayerHp;
    public GameObject obj1;//���ⲿ����Turtle
    private float canInjuryTime;//�����޵е���ȴʱ��
    private AudioSource attackSound;
    public bool canAttack;//�Ƿ���Թ�������ֹ����ʱ���Թ�����
    private Vector3 playerScale;
    private GameObject trunk;
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        attackSound = GetComponent<AudioSource>();
        maxPlayerHp = 60;//��ʼ������ֵ
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
        playerMove(direction);//��ɫ�ƶ�ʵ��
        playerjumping();//��ɫ��Ծ
        dash(direction);//���ʵ��       
        playerattack();//��ɫ��ͨ����
        playdeath();
        resetInjuryTime();//ˢ�����˺��޵е�ʱ��
    }  

    private void playerattack()//��ɫ�չ�
    {
        if(Input.GetMouseButtonDown(0) && !isattack && canAttack)//����isattack�Ļ��ᵼ���չ������
                                                                 //canAttack��ֹС�����������ʱ�����Թ���
        {
            isattack = true;
            combostep++;
            timer = interval;//��ʾ���¿�ʼ��ʱ����Ҫ���¿�ʼ��ʱʱ��time�ٴγ�ʼ��
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
       else if(timer<=0)//��������
            {
                combostep = 0;//��ʼ����ֵ
                //timer = 0;
            }
    }
    public void AttackOver()
    {
        isattack = false;
    }
  private void playerjumping()//��ɫ��Ծ
    {
 if ( Input.GetKeyDown(KeyCode.Space)&&jumpingNum>0) //��Ծʵ��//�������onground==true��Ϊ�����ͻᵼ�µڶ���������
        {
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
          if (direction > 0)//��ʹ��localscale��ʹ��flipx�Ļ����ᵼ�º��������չ���ײ���޷�����������ƶ����ı�
          {
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
    private void OnTriggerEnter2D(Collider2D other)  //�ӵ���ƽ̨��ײ���
    {
        if (other.gameObject.CompareTag("platform"))
        {
            onground = true;
            jumpingNum = 2;
        }
        if(canInjuryTime<=0)
        {
          if (other.gameObject.CompareTag("Trunk Bullet"))//�����ӵ�
           {
            obj1 = other.gameObject;
            playerHealth -= trunk.GetComponent<Trunk>().damageBullet; 
            animator.SetTrigger("isPlayerInjury");//���˶���
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
    private void OnCollisionStay2D(Collision2D collision)  //С�ֳ�����ײ���
    {
        
        if (canInjuryTime <= 0)//������˺���޵�ʱ��
        {
            if (collision.gameObject.CompareTag("Turtle"))
           {
            obj1 = collision.gameObject;
            playerHealth -= obj1.GetComponent<Turtle>().damage;//��������
            animator.SetTrigger("isPlayerInjury");//���˶���
            canInjuryTime = 1;//һ���ڷ���Ч���ĵط�����ʱ�����
                canAttack = false;
                Invoke("setcanAttack", 0.26f);
            }
            if(collision.gameObject.CompareTag("Trunk"))
            {
                obj1 = collision.gameObject;
                playerHealth -= obj1.GetComponent<Trunk>().damage;//��������
                animator.SetTrigger("isPlayerInjury");//���˶���
                canInjuryTime = 1;//һ���ڷ���Ч���ĵط�����ʱ�����
                canAttack = false;
                Invoke("setcanAttack", 0.26f);
            }
        }
    }
    private void resetInjuryTime()//�������޵�ʱ��д�ɺ���������д����ײ�������
                                  //Ϊ�˲�����ײʱ�����޵�ʱ�����
    {
        canInjuryTime -= Time.deltaTime;
    }
    private void playdeath()//�������
    {
        if(playerHealth<=0)
        {
           StartCoroutine(playerDeathAnimation());//Ҳ�����ڶ���������¼������
            speed = 0;//������ֵ
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

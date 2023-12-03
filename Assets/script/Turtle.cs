using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Turtle : Enemy
{
    public float speed = 3;//�ٶ�
    public float waitTime;//��ʱ��
    public Transform[] position;//��������
    private float wait;//���ڸ��µ�ʱ�� 
    private bool isright;//����
    private int i = 0;//�ı�����Ĳ���
    private Animator animator;
    public float attackTime;//������������ʱ��
    bool isPlayHitAnimat;
    public Transform playerTransform;
    public Animator repelEffectAnim;
    private string direction;
    private GameObject Player;
    public GameObject coin;//���Ԥ���������
    private bool isDie;//
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 0.5f;
        Player = GameObject.FindGameObjectWithTag ("Player");
        wait = waitTime;//����ʱ��       
        animator = GetComponent<Animator>();
        health = 30;//����С����ֵ
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        traverse();//С�������ƶ�
        print("С������" + health);
        TurtleDie();//С�������󱬽��
        allTimeCoundDown();//����ʱ������Ҫ��ʱ��time
    }
    private void allTimeCoundDown()
    {
        damageTime -= Time.deltaTime;//С�����˺�ʱ�䵹��ʱ

    }
    private void TurtleDie()//С������
    {
        if(health<=0)
        {
            isDie = true;//ȷ��С���Ѿ���������ֹ����ײ������������������ϵĽ��Ԥ����
            StartCoroutine(turtleDeathAnimation());
        }
    }
    IEnumerator turtleDeathAnimation()
    {
        animator.Play("turtle die");
        yield return new WaitForSeconds(0.24f);
        gameObject.SetActive(false);
    }
    private void traverse()//С�������ƶ�
    {
        transform.position = Vector2.MoveTowards(this.transform.position, position[i].position, speed * Time.deltaTime);//����˵�
        //����λ��
        if (Vector2.Distance(transform.position, position[i].position) <= 0.1f)
        {
           
            if(wait<=0)
            {
                if (i == 0)//����i
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                if (isright == false)//left
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);//ת��
                    direction = "left";
                    isright = true;
                }
                else//right
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);//ת��
                    direction = "right";
                    isright = false;
                } 
                wait = waitTime;//����ʱ��
            }
            else
            {
                wait -= Time.deltaTime;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.gameObject.CompareTag("Player"))//С���չ�����
        {
            player.isContactEnemy = true;
           StartCoroutine(CheckHitAnimation());
        }
        if(collision.gameObject.CompareTag ("playerattack") && damageTime<=0)
        {
            health -= Player.GetComponent<player>().playerDamage;//����С��Ѫ��
            bloodEffect();//��Ѫ��Ч
            animator.SetTrigger("isInjury");//С�����˶���
            damageTime = 0.3f;//����С�������޵�ʱ��
            if(direction=="right")//������Ч��ʵ��
            {
                if(transform.position.x-playerTransform.position.x>=0)//(�������
                {
                  repelEffectAnim.SetTrigger("isattack2"); 

                }
                  else//�������
                  {
                     repelEffectAnim.SetTrigger("isattack1");                
                } 
            }
            else if ((direction=="left"))
            {
                if (transform.position.x - playerTransform.position.x >= 0)//(������ң�
                {
                    repelEffectAnim.SetTrigger("isattack1");
                }
                else//�������
                {
                    repelEffectAnim.SetTrigger("isattack2");
                }
            }

            if (collision.gameObject.CompareTag("Player"))//(������ң�
            {
                StartCoroutine(CheckHitAnimation());//С���չ�����
            }
            if (collision.gameObject.CompareTag("playerattack") && (health - damageTime) <= 0&& !isDie)//����ǲ������һ��
            {
                 Instantiate(coin,transform.position,Quaternion.identity);//�������Ԥ����
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)//��ײ���
    {
       

    }
    IEnumerator CheckHitAnimation()//С����ͨ����Э��
        {
            animator.Play("Turtle Hit");//���Ź�������
            yield return new WaitForSeconds(attackTime);
        }
}
    
    


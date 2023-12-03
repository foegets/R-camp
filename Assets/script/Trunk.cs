using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Trunk : Enemy
{
    public Transform firePointTrans;
    public static bool isBulletTouchPlayer;
    public float damageBullet;//�ӵ��˺�
    public Transform playerTrans;//Ϊ��ȡ���λ�õ���Ϣ
    public Rigidbody2D playRg;//��������ܻ��󱻻���
    public float repelFouce;//������
    public Transform position1;//��������С��λ�Ʒ�Χ
    public Transform position2;
    public float speed;//�ƶ��ٶ� 
    public bool canHit;//������Ϣ�õ�boolֵ
    public   Animator animator;
    public float cd;//�ӵ���ȴʱ��
    public Transform playerTransform;
    private GameObject repelEffect;//������Ч�Ķ�������
    public GameObject Player;//Ϊ�˵�����ҽ�
    public GameObject coin;//�����ϷԤ���嵼��
    public GameObject bullet;//�ӵ�Ԥ���嵼��
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
        health = maxHp;//��ʼ����ֵ
        speed = 5;
        damageBullet = 5.0f;
        cd = 1.70f;
    }

    // Update is called once per frame
    void Update()
    {
        isDie();//�ж�������ʧ��
        moveToward();//�������
        isHit();//����
        damageTime -= Time.deltaTime;
        allTimeCoundDown();//����ʱ������Ҫ��ʱ��time
    }
    private void allTimeCoundDown()
    {
        damageTime -= Time.deltaTime;//С�����˺�ʱ�䵹��ʱ
    }
    private void isHit()
    {
        if (playerTrans.transform.position.y >= position1.position.y - 0.3f
           && playerTrans.transform.position.y <= position1.position.y + 0.3f)
        {
            animator.SetBool("isHit", true);//��������
        }
        else
        {
            animator.SetBool("isHit", false);
        }
    }
    public void activateBullet()
    {//��ָ��λ�ü���Ԥ����
             Instantiate(bullet, firePointTrans.position, Quaternion.identity);
    }
    private void moveToward()//�������
    {
        //�ж����λ���Ƿ��ڷ�Χ��
        if(playerTrans.position.x>=position2.position.x 
            && playerTrans.position.x <= position1.position.x 
            && playerTrans.position.y>=position1.position.y-0.7f 
            && playerTrans.position.y<=position1.position.y+15.0f)
        {
            //�������
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerTrans.position.x,this.transform.position.y), speed * Time.deltaTime);
            if(playerTrans.position.x-this.transform.position.x>=0)//����ת��ʵ��
            {
                transform.localScale = new Vector2(-startScale.x, startScale.y);//�ұ�
            }
            else if(playerTrans.position.x - this.transform.position.x < 0)
            {
                transform.localScale = startScale;//���       
            }
        }
    }
    private void isDie()
    {
        if(health<=0)
        {
            isdie = true;//ȷ��С���Ѿ���������ֹ����ײ������������������ϵĽ��Ԥ����
            animator.Play("trunk die");//���Ŷ���
            StartCoroutine(trunkDie());//�����ӳ�Я��   (Ҳ����ͨ����������¼��������
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
            health -= Player.GetComponent<player>().playerDamage;//��������
            bloodEffect();//��Ѫ����
            animator.SetTrigger("isInjury");//С�����˶���
            damageTime = 0.3f;//����ʱ��
            if (transform.position.x - playerTransform.position.x <= 0)//������Ч(�������
            {
                repelEffect.GetComponent<Animator>().SetTrigger("isattack1");
            }
            else//�������
            {
                repelEffect.GetComponent<Animator>().SetTrigger("isattack2");
            }
            if(collision.gameObject.CompareTag("playerattack") && (health-damageTime)<=0 && !isdie)//����ǲ������һ��
            {
                Instantiate(coin, transform.position, Quaternion.identity);//�������Ԥ����
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ײ���
    {
        //if (collision.gameObject.CompareTag("Player"))//ʹ��ұ�����
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
    private void OnCollisionStay2D(Collision2D collision)//��ײ���
    {
        //if (collision.gameObject.CompareTag("Player"))//ʹ��ұ�����
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

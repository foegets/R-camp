using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Trunk : Enemy
{
    public static bool isBulletTouchPlayer;
    public float damageBullet;//�ӵ��˺�
    public Transform playerTrans;//Ϊ��ȡ���λ�õ���Ϣ
    public Rigidbody2D playRg;//��������ܻ��󱻻���
    public float repelFouce;//������
    public Transform position1;//��������С��λ�Ʒ�Χ
    public Transform position2;
    public float speed;//�ƶ��ٶ�
    private SpriteRenderer spriteRenderer;
    public bool canHit;//������Ϣ�õ�boolֵ
    public   Animator animator;
    public Rigidbody2D bukketRigidbody2D;
public GameObject bullet;//��ȡ�����ӵ�
    public float cd;//�ӵ���ȴʱ��
    //����������ҵĽű�
    public coinInEnemy coinInTrunk1;
    public coinInEnemy coinInTrunk2;
    public coinInEnemy coinInTrunk3;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        maxHp = 20;
        health = maxHp;//��ʼ����ֵ
        speed = 5;
        damageBullet = 5.0f;
        bullet.isStatic = false;//��ʼ��boolֵ���ӵ���
        //print(position1.position.x);
        cd = 1.70f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        isDie();//�ж�������ʧ��
        moveToward();//�������
        isHit();//����
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
             //��������
                bullet.SetActive(true);            
                canHit = true;
                //����λ��
                bullet.transform.position = this.transform.position;
                if (spriteRenderer.flipX == true)//���ҷ���
                {
                  bukketRigidbody2D .velocity = Vector2.right * speed;
                }
                else //������
                {
                   bukketRigidbody2D .velocity = Vector2.left * speed;
                }
                cd = 1.70f;
                isBulletTouchPlayer = false;
            } 
        }

    }
    private void moveToward()//�������
    {
        //�ж����λ���Ƿ��ڷ�Χ��
        if(playerTrans.position.x>=position2.position.x 
            && playerTrans.position.x <= position1.position.x 
            && playerTrans.position.y>=position1.position.y-0.1f 
            && playerTrans.position.y<=position1.position.y+15)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerTrans.position.x,this.transform.position.y), speed * Time.deltaTime);//�������
            if(playerTrans.position.x-this.transform.position.x>=0)//����ת��ʵ��
            {
                spriteRenderer.flipX = true;//�ұ�
            }
            else if(playerTrans.position.x - this.transform.position.x < 0)
            {
                spriteRenderer.flipX = false;//���       
            }
        }
    }
    private void isDie()
    {
        if(health<=0)
        {
            coinInTrunk1.gameObject.SetActive(true);//�����Ҷ���
            coinInTrunk2.gameObject.SetActive(true);
            coinInTrunk3.gameObject.SetActive(true);
            coinInTrunk1.gameObject.transform.position = this.transform.position;//����λ��
            coinInTrunk2.gameObject.transform.position = this.transform.position;
            coinInTrunk3.gameObject.transform.position = this.transform.position;
            animator.Play("trunk die");//���Ŷ���
            StartCoroutine(trunkDie());//�����ӳ�Я��   
        }
    }
    IEnumerator trunkDie()
    {
        yield return new WaitForSeconds(0.5f);
        speed = 0;
        this.gameObject.SetActive(false);
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
        if(collision.gameObject.CompareTag("playerattack"))
        {
            print("�ɹ�");
            bloodEffect();
            Invoke("takeDamage", 0.16f);//ʹ��Ҷ������ٸı�boolֵ
            print("66");
        }
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
    public override void disReDamage()
    {
        base.disReDamage();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    public coinInEnemy coinInTurtle1;
    public coinInEnemy coinInTurtle2;
    public float speed = 3;//�ٶ�
    public float waitTime;//��ʱ��
    public Transform[] position;//��������
    private float wait;//���ڸ��µ�ʱ��
    private bool isright;//����
    private int i = 0;//�ı�����Ĳ���
    private Animator animator;
    public float attackTime;//������������ʱ��
    bool isPlayHitAnimat;
    // Start is called before the first frame update
    void Start()
    {
        //coinInTurtle = GetComponentInChildren<coinInTurtle>();
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
    }
    private void TurtleDie()//С������
    {
        if(health<=0)
        {
             
            coinInTurtle1.gameObject.SetActive(true);//�����Ҷ���
            coinInTurtle2.gameObject.SetActive(true);
            coinInTurtle1.gameObject.transform.position = this.transform.position;//����λ��
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
    private void traverse()//С�������ƶ�
    {
        //����λ��
        if (Vector2.Distance(transform.position, position[i].position) == 0)
        {
            if (wait > 0)
            {
                wait -= Time.deltaTime;//����ʱ��
            }
            else
            {
                if (i == 0)//����i
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                if (isright == false)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);//ת��
                    isright = true;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);//ת��
                    isright = false;
                }
                wait = waitTime;//����ʱ��
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, position[i].position, speed * Time.deltaTime);//����˵�
            print("1");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ײ���
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isContactEnemy = true;
           StartCoroutine(CheckHitAnimation());//С���չ�����
        }
        //if(collision.gameObject.CompareTag("playerattack"))
        //{
        //    animator.SetTrigger("isInjury");
        //    print("dfaff");
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)//��ײ���
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isContactEnemy = true;
            StartCoroutine(CheckHitAnimation());//С���չ�����
        }
        //if(collision.gameObject.CompareTag("playerattack"))
        //{
        //    animator.SetTrigger("isInjury");
        //    print("dfaff");
        //}
    }
    IEnumerator CheckHitAnimation()//С����ͨ����Э��
    {       
            animator.Play("Turtle Hit");//���Ź�������
            yield return new WaitForSeconds(attackTime);       
    }
}

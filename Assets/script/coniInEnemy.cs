using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinInEnemy : MonoBehaviour//С�ֽ�ҵ���ʵ��
{
    public Turtle turtle1;
    public Trunk trunk1;
    private Rigidbody2D rb;
    public float springFouce;//������
    public float fouceTime;//����ʱ��
    bool isactive;    //�ж��Ƿ񼤻�
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void isEnemyDie()
    {
        
        if (turtle1.health <= 0)//����ҿ�
        {
           fouceTime -= Time.deltaTime;//ʱ�����
            if (fouceTime >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, springFouce);//����
            }
        }
        if (turtle1.health <= 0)//����ҿ�
        {
            fouceTime -= Time.deltaTime;//ʱ�����
            if (fouceTime >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, springFouce);//����
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class trunkBullet : MonoBehaviour
{
    private GameObject trunkPosit;
    private GameObject player;
    public float speed;//�����ٶ�
    private Rigidbody2D rb;
    private Vector2 playerStartPosit;
    private Vector2 trunkStartPosit;
    public float activeTime;
    // Start is called before the first frame update
    void Start()
    {
        activeTime = 3.0f;
        trunkPosit = GameObject.FindGameObjectWithTag("Trunk");
        trunkStartPosit=trunkPosit.GetComponent<Transform>().position;//��ȡС�����ӵ�����ʱ�ĳ�ʼλ��
        player = GameObject.FindGameObjectWithTag("Player");
        playerStartPosit = player.transform.position;//��ȡ������ӵ�����ʱ�ĳ�ʼλ��
        rb = GetComponent<Rigidbody2D>();
        speed = 12;//��ʼ���ٶ�
    }

    // Update is called once per frame
    void Update()
    {
        islaunch();
        activeTimeOver();//ʱ�䵽��ʧ��
    }
    private void activeTimeOver()
    {
        activeTime -= Time.deltaTime;
        if(activeTime<=0)
        {
            Destroy(this.gameObject);//ʧ��
        }
    }
    private void islaunch()
    {
        if(playerStartPosit.x < trunkStartPosit.x)//�������
        {
            rb.velocity = Vector2.left * speed;
        }
        else//������ұ�
        {
            rb.velocity = Vector2.right * speed;
        }
          /*  rb.velocity = new Vector2(speed * (playerStartPosit.x - firePoint.GetComponent<Transform>().position.x), rb.velocity.y);*///������ҵķ���
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision. gameObject.CompareTag("Player"))//�������Ҳ��ʹ�ӵ���ʧ
        {
           Destroy(this.gameObject);//ʧ��
        }
    }
}

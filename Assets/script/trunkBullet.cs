using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class trunkBullet : MonoBehaviour
{
    public float speed;//�����ٶ�
    public GameObject trunk;
    private Rigidbody2D rg;
    public  bool isactive;//��ʾ�Ƿ񼤻�
    private float bulletCd;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        speed = 50;//��ʼ���ٶ�
        bulletCd = 3;
    }

    // Update is called once per frame
    void Update()
    {
        islaunch();
    }
    private void islaunch()
    {
       if(trunk.GetComponent<Trunk>().canHit==true)
        {
            //���Ź�������
            trunk.GetComponent<Trunk>(). animator.SetTrigger("isHit");
        }
        if(trunk.GetComponent<Trunk>().cd<0 )
        {
            this.gameObject.SetActive(false);
                  isactive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision. gameObject.CompareTag("Player"))//�������Ҳ��ʹ�ӵ���ʧ
        {
            this.gameObject.SetActive(false);
            isactive = false;
            Trunk.isBulletTouchPlayer = true;
        }
    }
}

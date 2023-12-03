using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coinInEnemy : MonoBehaviour//С�ֽ�ҵ���ʵ��
{
    private Rigidbody2D rb;
    public float springSpeed;//������
    public float springTime;//����ʱ��
    bool isactive;    //�ж��Ƿ񼤻�
    private GameObject player;
    public float speed;//����ƶ�����ҵ��ٶ�
    public float staticTime;//��ֹһ��ʱ�����������
    private Collider2D coinColli;
    // Start is called before the first frame update
    void Start()
    {
        springTime = 0.5f;
        springSpeed = 3.0f;
        coinColli = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        springTime -= Time.deltaTime;
        if(staticTime<=0)
        {
            coinColli.forceReceiveLayers =LayerMask.NameToLayer("Nothing") ;
        //�������
          transform.position = Vector3.MoveTowards(this.transform.position, player.GetComponent<Transform>().position, speed * Time.deltaTime);
        }
        else//����������֮ǰ
        {
            staticTime -= Time.deltaTime;
            if(springTime>=0)
            {
            coinBounce();
            } 
        }

    }
    private void coinBounce()//��ҵ��䵯����
    {
       rb.velocity = new Vector2(rb.velocity.x, springSpeed);//����
    }
}

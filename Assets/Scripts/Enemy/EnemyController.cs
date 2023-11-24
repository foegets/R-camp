using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;//移动速度
    private Rigidbody2D enemyRb;
    public GameObject enemyPoint1;//左边点
    public GameObject enemyPoint2;//右边点
    private int direction = 2;//调整朝向
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
    }
    public void Flip()
    {
        if (Vector2.Distance(transform.position, enemyPoint1.transform.position) < 0.5f)//检测与左边点距离
        {
            direction = -2;
            transform.localScale = new Vector3(direction, 2, 2);//转向
        }
        if (Vector2.Distance(transform.position, enemyPoint2.transform.position) < 0.5f)//检测与右边点距离
        {
            direction = 2;
            transform.localScale = new Vector3(direction, 2, 2);//转向
        }
    }
    public void Move()
    {
        enemyRb.velocity = new Vector2(-direction*moveSpeed, 0);//给敌人一个移动速度
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")//角色撞到敌人失败重来
        {
            Destroy(collision.gameObject);
            SceneManager.LoadSceneAsync(2);
        }
    }
}

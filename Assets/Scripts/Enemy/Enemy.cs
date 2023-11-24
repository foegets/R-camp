using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float attackRange = 2.5f;
    public float detectionRange = 10f;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        int faceDir = (int)transform.localScale.x;

        //人物翻转
        transform.localScale = new Vector3(faceDir, 1, 1);

        if (distance <= detectionRange)
        {
            // 当目标在检测范围内时，追踪玩家
            Vector3 direction = (target.position - transform.position).normalized;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

            if (distance <= attackRange)
            {
                // 当敌人与玩家距离小于攻击距离时，停下并执行攻击动画
                rb.velocity = Vector3.zero;
                GetComponent<Animator>().SetTrigger("boarAttack");
            }
            else
            {
                // 否则，执行跑动动画
                GetComponent<Animator>().SetTrigger("boarRun");
            }
        }
        else
        {
            // 当目标不在检测范围内时，执行待机动画
            GetComponent<Animator>().SetTrigger("boaridle");
        }
    }
}

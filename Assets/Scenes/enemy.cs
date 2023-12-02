using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class enemy : MonoBehaviour
{

    public float HP;
    public float maxHP;
    public Transform target;
    public float enemyMoveSpeed;
    public float followDistance;


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        if (Mathf.Abs(transform.position.x-target.position.x) < followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemyMoveSpeed * Time.deltaTime);
            if (transform.position.x - target.position.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);
            if (transform.position.x - target.position.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Destroy(gameObject);

        }
    }

}

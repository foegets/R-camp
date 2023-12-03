using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyNasbr : MonoBehaviour
{
    //设置属性
    public int health;
    public int damage;
    public float speed;
    public float waitTime;
    public Transform[] movePos;

    //设置动画
    public SkeletonAnimation enemyAnimation;
    public AnimationReferenceAsset idle, move, attack;
    private string currentState;

    private int i = 0;//定位数组的游标

    private bool moveRight;
    private float wait;

    private Rigidbody2D enemyRigidbody;

    private enum Direction
    {
        Right = 1,
        Left = -1,
        Stop = 0
    }

    private Direction moveDir;

    // Start is called before the first frame update
    void Start()
    {
        //初始化数据
        moveRight = true;
        moveDir = Direction.Right;
        wait = waitTime;
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAnimation(enemyAnimation);
        Move();
    }

    void Move()
    {
        Vector2 enemyVel;
        switch (moveDir)
        {
            case Direction.Right:
                enemyVel = new Vector2( 1 * speed, 0f);
                enemyRigidbody.velocity = enemyVel;
                AnimationSet(enemyAnimation, move, true, 1f);
                break;
            case Direction.Left:
                enemyVel = new Vector2( -1 * speed, 0f);
                enemyRigidbody.velocity = enemyVel;
                AnimationSet(enemyAnimation, move, true, 1f);
                break;
            case Direction.Stop:
                enemyVel = new Vector2(0f, 0f);
                enemyRigidbody.velocity = enemyVel;
                AnimationSet(enemyAnimation, idle, true, 1f);
                break;
        }

        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                moveDir = Direction.Stop;
            }
            else
            {
                if (moveRight)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    moveRight = false;
                    moveDir = Direction.Left;
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    moveRight = true;
                    moveDir = Direction.Right;
                }
                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                waitTime = wait;
            }

        }

    }

    void GetAnimation(SkeletonAnimation skeletonAnimation)
    {
        //获取正在播放的动画
        currentState = skeletonAnimation.AnimationName;
    }
    void AnimationSet(SkeletonAnimation skeletonAnimation, AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentState))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

    //受伤
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

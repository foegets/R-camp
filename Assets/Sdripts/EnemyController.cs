using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform LeftPoint,RightPoint;

    private bool movingRight;

    public float JumpForce;

    private Rigidbody2D theRb;

    public Animator anim;

    public SpriteRenderer theSR;

    public float moveTime, waitTime;

    private float moveCount, waitCount;

    private Spikes Spikes;
    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody2D>();

        LeftPoint.parent = null;
        RightPoint.parent = null;

        movingRight = false;

        moveCount = moveTime;
        theSR.flipX =true ;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("MoveCount", moveCount);
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
            if (movingRight)
            {
                theRb.velocity = new Vector2(moveSpeed, theRb.velocity.y);
                if (transform.position.x > RightPoint.position.x)
                {
                    theSR.flipX = false;
                    movingRight = false;

                }
            }
            else
            {

                theRb.velocity = new Vector2(-moveSpeed, theRb.velocity.y);
                if (transform.position.x < LeftPoint.position.x)
                {
                    theSR.flipX = true;
                    movingRight = true;
                }

            }
            if (moveCount<=0)
            {
                waitCount = waitTime;
            }
        }
        else
        {
            if (waitCount>0)
            {
                waitCount -= Time.deltaTime;
                theRb.velocity = new Vector2(0, theRb.velocity.y);

                if (waitCount<=0)
                {
                    moveCount = moveTime;
                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
           Spikes.instance.isDied = true;
            Spikes.instance.DIE();
            Time.timeScale = 0f;
        }



    }

}

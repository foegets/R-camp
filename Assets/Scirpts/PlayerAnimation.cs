using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements.Experimental;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isMove = false;
    //private bool isOnWall = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        player= GetComponent<Player>();
    }

    private void Update()
    {
        SetAnim();
    }
    //µÅÇ½Ìø¹¦ÄÜ
    /*private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.CompareTag("Wall"))
        {

            isOnWall = true;
            rb.gravityScale = 0.1f;
            //Debug.Log(CompareTag("Wall"));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {

            isOnWall = false;
            rb.gravityScale = 1f;
            //Debug.Log(CompareTag("Wall"));
        }
    }*/

    public void SetAnim()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isMove = true;
            //Debug.Log(ismove);
        }
        else
        {
            isMove = false;
            //Debug.Log(ismove);
        }
        animator.SetBool("ismove",isMove);
        animator.SetFloat("Velocity_Y", rb.velocity.y);
        animator.SetBool("isOnGround", player.isOnGround);
    }
}

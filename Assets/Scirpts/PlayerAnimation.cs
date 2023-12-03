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
        animator.SetFloat("velocity_Y", rb.velocity.y);
        animator.SetBool("isGround", player.isOnGround);
        animator.SetBool("isDead", player.isDead);
        animator.SetBool("isAttack", player.isAttack);
        //animator.SetInteger("combo", player.combo);
        //Debug.Log(player.isOnGround);
    }

    public void Playerhurt()
    {
        animator.SetTrigger("hurt");
        //Debug.Log("fuck");
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("attack");
    }

}

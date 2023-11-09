using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private PlayerController pc;

    private PhysicsCheck pk;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        pk = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        setAnimation();
    }

    public void setAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isWalk", pc.isWalk);
        anim.SetBool("canRun", pc.canRun);
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", pk.isGround);
    }
}

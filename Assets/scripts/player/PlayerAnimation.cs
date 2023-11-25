using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private PlayerController pc;

    private PhysicsCheck pk;

    private Character character;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        pk = GetComponent<PhysicsCheck>();
        character = GetComponent<Character>();
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
        anim.SetBool("isInvulnerable", character.invulnerable);
        anim.SetBool("isDead", character.isdead);
    }

    public void Playhurt()
    {
        anim.SetTrigger("hurt");
    }
}

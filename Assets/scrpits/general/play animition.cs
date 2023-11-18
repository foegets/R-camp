using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playanimition : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private physicalcheck physicalcheck;
    private Playercontrol playercontrol;
    // Start is called before the first frame update
    private void Awake()
    {
            anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicalcheck=GetComponent<physicalcheck>();
        playercontrol=GetComponent<Playercontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocity.y",rb.velocity.y);
        anim.SetBool("isground", physicalcheck.isGround);
        anim.SetBool("isdead", playercontrol.isdead);
        anim.SetBool("isattack", playercontrol.isattack);
    }
    public void PlayHurt()
    {
        anim.SetTrigger("Hurt");

    }
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityx",Mathf.Abs(rb.velocity.x));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playanimition : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private physicalcheck physicalcheck;
    // Start is called before the first frame update
    private void Awake()
    {
            anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicalcheck=GetComponent<physicalcheck>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocity.y",rb.velocity.y);
        anim.SetBool("isground", physicalcheck.isGround);
    }
}

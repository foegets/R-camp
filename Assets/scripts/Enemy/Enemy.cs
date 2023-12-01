using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    Animator anim;

    public int i = 0;

    [Header("基本参数")]

    public float normalSpeed;
    public float chaseSpeed;
    // Start is called before the first frame update
    public float hurtForce;
    public Transform attacker;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        if(attackTrans.position.x- transform.position.x > 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            if (i == 0) i = 1;
            
        }


        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (i == 1) i = 0;
            
        }
        isHurt = true;
        anim.SetTrigger("Hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;

        rb.AddForce(dir*hurtForce ,ForceMode2D.Impulse);
        isHurt=false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("isDead", true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}

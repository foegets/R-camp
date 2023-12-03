using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float weaponDamage;

    public float weaponAttackRange;  

    public float weaponAttackRate;

    protected Rigidbody2D rb;

    protected new Collider2D collider2D;

    protected Animator animator;
    // Start is called before the first frame update

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual GameObject BePick()
    {
        return this.gameObject;
    }
}

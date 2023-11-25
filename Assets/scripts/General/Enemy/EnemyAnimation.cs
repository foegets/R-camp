using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private Character character;
    private new Rigidbody2D  rigidbody2D;
    private Enemy enermy;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        enermy = GetComponent<Enemy>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        animator.SetBool("isDead", character.isdead);
        animator.SetBool("isWalk", enermy.isWalk);
    }
}

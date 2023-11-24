using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator enemyAnimator;
    private Rigidbody2D enemyRb;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnim();
    }
    public void SetAnim()
    {
        enemyAnimator.SetFloat("Run", Mathf.Abs(enemyRb.velocity.x));
    }
}

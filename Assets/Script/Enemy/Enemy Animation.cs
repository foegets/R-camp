using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    private bool Toward=true;
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetBool("Back", Toward);//取绝对值关键字Mathf.Abs
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "back")
        {
            Toward = !Toward;
          
        }//要做到碰到collision之后彻底改变运动性质
    }
}

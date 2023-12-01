using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));//取绝对值关键字Mathf.Abs
    }
    public void GetHurt()
    {
        anim.SetTrigger("Hurt");
        Debug.Log(1);
    }
}

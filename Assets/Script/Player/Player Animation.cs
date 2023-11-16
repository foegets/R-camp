using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public Rigidbody2D rb;
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetFloat("velocityY", Mathf.Abs(rb.velocity.y));
        }
        else
        {
            anim.SetFloat("velocityY", 0);
        }
        //取绝对值关键字Mathf.Abs
    }//啊米诺斯
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public int Direction=1;
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetInteger("Toward",Direction);
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("back"))
        {
            Direction=-Direction;
        }//要做到碰到collision之后彻底改变运动性质
    }
}

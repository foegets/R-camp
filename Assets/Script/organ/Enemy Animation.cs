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
        }//Ҫ��������collision֮�󳹵׸ı��˶�����
    }
}

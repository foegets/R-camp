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
        anim.SetBool("Back", Toward);//ȡ����ֵ�ؼ���Mathf.Abs
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "back")
        {
            Toward = !Toward;
          
        }//Ҫ��������collision֮�󳹵׸ı��˶�����
    }
}

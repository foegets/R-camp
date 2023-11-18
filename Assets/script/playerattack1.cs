using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattack1 : MonoBehaviour
{
    private PolygonCollider2D polygon1;
    public SpriteRenderer spriteRenderer;
    //public PolygonCollider2D polygon2;
    public float time;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        polygon1 = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !spriteRenderer.flipX)//ÓÒ·½Ïò¹¥»÷Åö×²Ìå¼¤»î
        {
            polygon1.enabled = true;
            //print(polygon1.enabled);
            StartCoroutine(disableAttack1());
        }

     
    }
    IEnumerator disableAttack1()//ÓÒ·½Ïò¹¥»÷Åö×²ÌåÊ§»î
    {
        yield return new WaitForSeconds(time);
        polygon1.enabled = false;
        //print(polygon1.enabled);
    }
 
    //Åö×²¼ì²â
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Åö×²¼ì²â
        if (collision.gameObject.CompareTag("Turtle"))
        {
            collision.gameObject.GetComponent<Turtle>().health -= player.playerDamage;
            animator = collision.gameObject.GetComponent<Animator>();
            animator.SetTrigger("isInjury");
            print("124");
        }
        if (collision.gameObject.CompareTag("Trunk"))
        {
            collision.gameObject.GetComponent<Trunk>().health -= player.playerDamage;
            animator = collision.gameObject.GetComponent<Animator>();
            animator.SetTrigger("isInjury");
            print("124ddfas");
        }
    }
}

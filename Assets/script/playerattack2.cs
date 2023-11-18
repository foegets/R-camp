using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class playerattack2 : MonoBehaviour
{
    public float time;
    private Animator animator;
    private PolygonCollider2D polygon2;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        polygon2 = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spriteRenderer.flipX)//×ó·½Ïò¹¥»÷Åö×²Ìå¼¤»î
        {
            polygon2.enabled = true;
            //print(polygon1.enabled);
            StartCoroutine(disableAttack2());
        }
    }
    IEnumerator disableAttack2()//×ó·½Ïò¹¥»÷Åö×²ÌåÊ§»î
    {
        yield return new WaitForSeconds(time);
        polygon2.enabled = false;
        //print(polygon1.enabled);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Åö×²¼ì²â
        if (collision.gameObject.CompareTag("Turtle"))
        {
            collision.gameObject.GetComponent<Turtle>().health -= player.playerDamage;
            animator = collision.gameObject.GetComponent<Animator>();
            animator.SetTrigger("isInjury");
            print("124ddfas");
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


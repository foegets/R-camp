using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class playerattack2 : MonoBehaviour
{
    
    public float time;
    private Animator animator;
    //private PolygonCollider2D polygon2;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //polygon2 = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& spriteRenderer.flipX*/ && gameObject.GetComponentInParent<player>().canAttack == false)//左方向攻击碰撞体激活
        {
            //polygon2.enabled = true;
            StartCoroutine(disableAttack2());
        }
        print(gameObject.GetComponentInParent<player>().canAttack);
    }
    IEnumerator disableAttack2()//左方向攻击碰撞体失活
    {
        yield return new WaitForSeconds(time);
        //polygon2.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
           //碰撞检测（玩家攻击小怪）
        if (collision.gameObject.CompareTag("Turtle")&&collision.gameObject.GetComponent<Turtle>().damageTime<=0)
        {
            collision.gameObject.GetComponent<Turtle>().health -= player.playerDamage;
            animator = collision.gameObject.GetComponent<Animator>();
             animator.SetTrigger("isInjury");
            collision.gameObject.GetComponent<Turtle>().bloodEffect();//流血
            //Invoke("attackCd", 0.16f);
            collision.gameObject.GetComponent<Turtle>().disReDamage();//重置小怪的受伤cd
            gameManager.cameraShake.Shake();//屏幕震动
            print("124ddfas");
        }
        if (collision.gameObject.CompareTag("Trunk") && collision.gameObject.GetComponent<Trunk>().damageTime <= 0)
        {
            collision.gameObject.GetComponent<Trunk>().health -= player.playerDamage;
            animator = collision.gameObject.GetComponent<Animator>();
            if (!collision.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("isInjury");
            }
            collision.gameObject.GetComponent<Trunk>().bloodEffect();//流血
            collision.gameObject.GetComponent<Trunk>().disReDamage();//重置小怪的受伤cd
            gameManager.cameraShake.Shake();//屏幕震动
        }
 
    }
}


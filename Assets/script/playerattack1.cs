using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerattack1 : MonoBehaviour
{
    private PolygonCollider2D polygon1;
    public SpriteRenderer spriteRenderer;
    public float time;
    private Animator animator;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    //碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞检测
        if (collision.gameObject.CompareTag("Turtle") && collision.gameObject.GetComponent<Turtle>().damageTime <= 0)
        {
            collision.gameObject.GetComponent<Turtle>().health -= player.playerDamage;
            animator = collision.gameObject.GetComponent<Animator>();
            if (!collision.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("isInjury");
            }
            collision.gameObject.GetComponent<Turtle>().bloodEffect();//流血
            collision.gameObject.GetComponent<Turtle>().disReDamage();//重置小怪的受伤cd
            gameManager.cameraShake.Shake(); //屏幕震动
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
            gameManager.cameraShake.Shake(); //屏幕震动
        }
    }
}

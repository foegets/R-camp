using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private Rigidbody2D rb;
    private BombTrap bt;
    public GameObject Trap1;
    public PlayerController pc;
    public static bool shootOffTrigger;
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        bt = GetComponent<BombTrap>();
    }
    private void Update(){
        StartCoroutine(ShootOffCheck());
    }
    IEnumerator ShootOffCheck(){
            //弹射陷阱
        if(shootOffTrigger){
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(transform.up * 25, ForceMode2D.Impulse);
            shootOffTrigger = false;
        }
        yield return new WaitForSeconds(0.01f);
    }
}
    //想办法让携程只在特定的时候触发减少计算量

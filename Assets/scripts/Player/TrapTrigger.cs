using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private Rigidbody2D rb;
    private BombTrap bt;
    private JumpPlus jp;
    public GameObject Trap1;
    public GameObject JumpPlus1;
    public PlayerController pc;
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
    }
    private void Update(){
        StartCoroutine(ShootOffCheck());
        StartCoroutine(JumpPlusCheck());
    }
    IEnumerator ShootOffCheck(){
            //弹射陷阱
        bt = GetComponent<BombTrap>();
        bool shootOffTrigger = Trap1.GetComponent<BombTrap>().shootOffTrigger;
        if(shootOffTrigger){
            rb.velocity = new Vector2(0f,0f);
            rb.AddForce(transform.up*25,ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.25f);
    }
    IEnumerator JumpPlusCheck(){
            //连跳+1
        jp = GetComponent<JumpPlus>();
        bool jumpPlusTrigger = JumpPlus1.GetComponent<JumpPlus>().jumpPlusTrigger;
        if(jumpPlusTrigger){
            pc.jumpable += 1;
            Destroy(JumpPlus1);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.25f);
    }
}
    //想办法让携程只在特定的时候触发减少计算量

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlus : MonoBehaviour
{
    //连跳+1
    private Collider2D cd;
    private PlayerController pc;
    public bool jumpPlusTrigger;
    private void Awake(){
        cd = GetComponent<Collider2D>();

    }
    public void OnTriggerEnter2D(Collider2D Player){
        jumpPlusTrigger = true;
        Player.GetComponent<PlayerController>().jumpable += 1;
        Destroy(gameObject,0.01f);
    }
}

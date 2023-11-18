using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlus : MonoBehaviour
{
    //连跳+1
    private Collider2D cd;
    public PlayerController pc;
    public bool jumpPlusTrigger;
    private void Awake(){
        cd = GetComponent<Collider2D>();
        pc = GetComponent<PlayerController>();
    }
    public void OnTriggerEnter2D(Collider2D Player){
        jumpPlusTrigger = true;
        Destroy(gameObject,0.01f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    //弹射陷阱
    private Collider2D cd;
    public bool shootOffTrigger;
    private void Start(){
        cd = GetComponent<Collider2D>();
    }
    public void OnTriggerEnter2D(Collider2D Player){
        TrapTrigger.shootOffTrigger = true;
        Destroy(gameObject);
    }
}

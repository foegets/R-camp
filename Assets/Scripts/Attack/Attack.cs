using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;//伤害
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)//碰撞造成伤害
    {
        collision.GetComponent<Charactor>()?.TakeDamage(this);//？用于判断碰撞物体是否带有Charactor组件
    }
    public void GetSword()
    {
        damage += 10;
    }
}

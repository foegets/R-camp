using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public PlayerController playercontroller;
    public Attack attack;
    public Item longSword;
    public Vector2[] modifiedPoints1 = new Vector2[] {
    new Vector2((float)-1.05560374,(float)2.44774437),
    new Vector2((float)-1.49212921,(float)1.34455848),
    new Vector2((float)2.17647982,(float)0.166598439),
    new Vector2((float)4.11107874,(float)0.589822054),};
    public Vector2[] modifiedPoints2 = new Vector2[] {
        new Vector2((float)1.47600317,(float)2.47689438),
        new Vector2((float)0.51643914,(float)2.42989731),
        new Vector2((float)-0.363341808,(float)1.98333931),
        new Vector2((float)-0.97833091,(float)1.02692032),
        new Vector2((float)-0.670362949,(float)-0.397018671),
        new Vector2((float)2.70233274,(float)-0.427325249),
        new Vector2((float)2.71679115,(float)-0.374915004),
        new Vector2((float)2.93884254,(float)0.476438761),
        new Vector2((float)2.68471169,(float)1.92382622),};
    public Vector2[] modifiedPoints3 = new Vector2[] {
        new Vector2((float)0.444645882,(float)3.09318161),
        new Vector2((float)-0.734291255,(float)2.29365134),
        new Vector2((float)-0.252158463,(float)1.23181152),
        new Vector2((float)0.161989629,(float)0.573150754),
        new Vector2((float)0.518595397,(float)-0.606375694),
        new Vector2((float)1.85907769,(float)-0.740192175),
        new Vector2((float)2.80731821,(float)-0.306013107),
        new Vector2((float)2.79399419,(float)0.952037811),
        new Vector2((float)2.44883323,(float)1.8891449),
        new Vector2((float)1.73780262,(float)2.73245049)};
    private void Awake()
    {
        attack = GetComponent<Attack>();
        playercontroller = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BigSword")) // 如果碰撞的游戏对象tag为"BigSword"，增伤
        {
            foreach (var child in GetComponentsInChildren<Attack>())
            {
                child.damage += 1;
            }
        }
        if (other.gameObject.CompareTag("Shoes")) // 如果碰撞的游戏对象tag为"Shoes"，加速
        {
            playercontroller.speed += 100f;
        }
        //盔甲（减伤）在执行受伤（Character-TakeDamage）时生效
        if (other.gameObject.CompareTag("LongSword")) // 如果碰撞的游戏对象tag为"LongSword"，增大攻击范围
        {
            GameObject attack1 = GameObject.Find("Attack1");
            GameObject attack2 = GameObject.Find("Attack2");
            GameObject attack3 = GameObject.Find("Attack3");
            PolygonCollider2D polygonCollider1 = attack1.GetComponent<PolygonCollider2D>();
            PolygonCollider2D polygonCollider2 = attack2.GetComponent<PolygonCollider2D>();
            PolygonCollider2D polygonCollider3 = attack3.GetComponent<PolygonCollider2D>();
            polygonCollider1.enabled = true;
            polygonCollider2.enabled = true;
            polygonCollider3.enabled = true;
            if (polygonCollider1 != null)
            {
                // 修改点信息
                polygonCollider1.points = modifiedPoints1;
            }
            if (polygonCollider2 != null)
            {
                // 修改点信息
                polygonCollider2.points = modifiedPoints2;
            }
            if (polygonCollider3 != null)
            {
                // 修改点信息
                polygonCollider3.points = modifiedPoints3;
            }
            polygonCollider1.enabled = false;
            polygonCollider2.enabled = false;
            polygonCollider3.enabled = false;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnCollisonEnter : MonoBehaviour
{
    // 从外部获取爆炸物体
    public GameObject Prefab_Bomb;
    // 玩家操控对象
    public Rigidbody player;
    // 爆炸力度
    public float ExplosionForce;
    // 爆炸半径
    public float ExplosionRad;
    // 爆炸的向上推力
    public float ExplosionUpforce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 监听是否碰撞
    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate(要生成的物体，生成位置，生成时旋转角度)
        Instantiate(Prefab_Bomb,transform.position,Quaternion.identity);// identity的旋转角度为0
        player.AddExplosionForce(ExplosionForce, transform.position, ExplosionRad, ExplosionUpforce, ForceMode.Impulse);
        Destroy(Prefab_Bomb,0.6f);// 消除爆炸物体
        Destroy(gameObject);// 爆炸完后消除本身
    }
    
}

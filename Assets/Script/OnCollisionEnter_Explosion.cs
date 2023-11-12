using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 碰地爆炸 : MonoBehaviour
{
    // 从外部获取爆炸物体
    public GameObject Prefab_Bomb;
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
        Destroy(gameObject);// 爆炸完后消除本身
        StartCoroutine(DelayExecute(3.0f));

    }
    IEnumerator DelayExecute(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Destroy(Prefab_Bomb);  
        
    }
}

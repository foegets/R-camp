using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.cameraShake = GameObject.FindGameObjectWithTag("cameraShake").GetComponent<cameraShake>();//初始化震动脚本
    }
    void LateUpdate()
    {
        //相机跟随实现
        if(target!=null)
        {
            if(transform.position!=target.position)
            {
                Vector2 targetplayer = target.position;
                this.transform.position = Vector2.Lerp(transform.position, targetplayer, smoothing);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

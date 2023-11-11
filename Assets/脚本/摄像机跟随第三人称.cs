using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s : MonoBehaviour
{
    // 玩家对象  
    public Transform player;
    // 摄像机距离玩家的高度 
    public float cameraHeight = 5.0f;  
    // 摄像机的俯视角度 
    public float tiltAngle = 45.0f;
    // 摄像机距离玩家的距离 
    public float distance = 5.0f;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        // 摄像机的目标位置设定为玩家上方，并斜向俯视的位置  
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z - distance);
        transform.position = targetPosition;

        // 通过Quaternion的LookRotation函数设定摄像机的朝向，确保玩家始终在屏幕中央  
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0; // 忽略y轴，只考虑水平和垂直方向  
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up); // 使摄像机始终朝向玩家  
        rotation *= Quaternion.Euler(tiltAngle, 0, 0); // 添加俯视角度  
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10); // 平滑过渡到目标旋转
    }

    
}

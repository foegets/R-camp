using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow_Third : MonoBehaviour
{
    // 玩家对象  
    public Transform player;  
    // 摄像机位置
    Vector3 targetPosition;
    // 判断是否被鼠标拖拽
    bool isDragging = false;
    // 拖拽旋转的速度
    float DragSpeed = 5.0f;
    // 初始偏移量
    Vector3 offset;
    // 玩家与摄像机之间的距离
    float distance;

    void Start()
    {
        // 先初始化摄像机位置
        targetPosition = new Vector3(player.position.x, player.position.y + 4, player.position.z + 6);
        transform.position = targetPosition;

        offset = transform.position - player.position;
    }
    // Update is called once per frame
    void Update()
    {
        // 保持摄像机距离玩家的距离不变  
        transform.position = player.position + offset;

        // 看向玩家位置
        transform.LookAt(player.position);
    }
}

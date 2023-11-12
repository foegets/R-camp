using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow_Third : MonoBehaviour
{
    // ��Ҷ���  
    public Transform player;  
    // �����λ��
    Vector3 targetPosition;
    // �ж��Ƿ������ק
    bool isDragging = false;
    // ��ק��ת���ٶ�
    float DragSpeed = 5.0f;
    // ��ʼƫ����
    Vector3 offset;
    // ����������֮��ľ���
    float distance;

    void Start()
    {
        // �ȳ�ʼ�������λ��
        targetPosition = new Vector3(player.position.x, player.position.y + 4, player.position.z + 6);
        transform.position = targetPosition;

        offset = transform.position - player.position;
    }
    // Update is called once per frame
    void Update()
    {
        // ���������������ҵľ��벻��  
        transform.position = player.position + offset;

        // �������λ��
        transform.LookAt(player.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s : MonoBehaviour
{
    // ��Ҷ���  
    public Transform player;
    // �����������ҵĸ߶� 
    public float cameraHeight = 5.0f;  
    // ������ĸ��ӽǶ� 
    public float tiltAngle = 45.0f;
    // �����������ҵľ��� 
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
        // �������Ŀ��λ���趨Ϊ����Ϸ�����б���ӵ�λ��  
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z - distance);
        transform.position = targetPosition;

        // ͨ��Quaternion��LookRotation�����趨������ĳ���ȷ�����ʼ������Ļ����  
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0; // ����y�ᣬֻ����ˮƽ�ʹ�ֱ����  
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up); // ʹ�����ʼ�ճ������  
        rotation *= Quaternion.Euler(tiltAngle, 0, 0); // ��Ӹ��ӽǶ�  
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10); // ƽ�����ɵ�Ŀ����ת
    }

    
}

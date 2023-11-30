using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped_Door : MonoBehaviour
{
    // ��ת�ٶ�
    public float RotSpeed = 30f;
    // ��������
    public float hitForce = 50.0f;
    // ��ȡ������
    Rigidbody rb;
    CharacterController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        
    }
    void Update()
    {
        Vector3 CurAngle = transform.eulerAngles;
        CurAngle.y += RotSpeed * Time.deltaTime;
        transform.eulerAngles = CurAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            playerController = other.gameObject.GetComponent<CharacterController>();
            Vector3 HitDir = transform.position - other.transform.position + Vector3.up;
            if (rb != null)
            {
                rb.AddForce(HitDir * hitForce, ForceMode.Impulse);
            }
            else if (playerController != null)
            {

                playerController.SimpleMove(HitDir * hitForce);
            }
            else
            {
                Debug.Log("�������û����Ч����ײ���");
            }
        }
    }
 
}

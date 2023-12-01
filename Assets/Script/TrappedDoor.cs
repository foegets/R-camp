using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped_Door : MonoBehaviour
{
    // ��ת�ٶ�
    public float RotSpeed = 30f;
    // ��������
    public float hitForce = 50.0f;
    // �ж��Ƿ�����ײ
    public bool isCollid;
    // ���ɷ���
    Vector3 hitDir;
    // ��ȡ������
    Rigidbody rb;
    CharacterController playerController;
    // Start is called before the first frame update
    void Start()
    {
        isCollid = false;
    }

    private void FixedUpdate()
    {
        if (isCollid)
        {
            if (rb != null)
            {
                rb.AddForce(hitDir.normalized * hitForce, ForceMode.Impulse);
            }
            else if (playerController != null)
            {
                // ��֪��,��ֻ֪��SimpleMove����û��
            }
            else
            {
                print("û�м�⵽��Ч����ײ���");
            }
            isCollid = false;
        }
    }
    void Update()
    {
        Vector3 CurAngle = transform.eulerAngles;
        CurAngle.y += RotSpeed * Time.deltaTime;
        transform.eulerAngles = CurAngle;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            playerController = collision.gameObject.GetComponent<CharacterController>();
            // ���ײ������˶���������ײ������
            hitDir = rb.GetPointVelocity(collision.contacts[0].point);
            hitDir.y = 0;
            isCollid = true;
        }
    }


    
 
}

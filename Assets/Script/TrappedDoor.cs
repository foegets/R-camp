using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapped_Door : MonoBehaviour
{
    public float hitForce = 10.0f;
    Rigidbody rb;
    CharacterController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            playerController = collision.gameObject.GetComponent<CharacterController>();
            // ����ŵ���ת�ٶ�
            float DoorRotSpeed = Mathf.Abs(transform.rotation.eulerAngles.y);
            // ���������ٶȺͷ���
            Vector3 ForceDir = transform.right;
            float ForceStrength = DoorRotSpeed * hitForce;
            Vector3 Force = ForceDir * ForceStrength;
            if (rb != null)
            {
                rb.AddForce(Force, ForceMode.Impulse);
            }
            else if (playerController != null)
            {
                playerController.SimpleMove(Force);
            }
        }
    }
}

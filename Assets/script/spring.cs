using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour
{
    //���ɲ���
    public float ricochetOff = 40.0f;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //���е��ɺͼ��
        if (other.gameObject.CompareTag("Player"))
        {
            other.attachedRigidbody.velocity = new Vector2(other.attachedRigidbody.velocity.x, ricochetOff);
            player.onground = false;
            player.jumpingNum = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

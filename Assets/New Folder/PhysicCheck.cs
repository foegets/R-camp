using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{
    public bool isGround;//�Ƿ��ڵ�����
    public bool isboom;//�Ƿ�����ը��
    public float checkRaduisJump;
    public float checkRaduisBoom;
    public LayerMask groundLayer;
    public LayerMask boomLayer;



    public void Check()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduisJump, groundLayer);
        isboom = Physics2D.OverlapCircle(transform.position, checkRaduisBoom, boomLayer);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
}

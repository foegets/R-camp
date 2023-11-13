using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToChangeDirection : MonoBehaviour
{
    // 旋转速度  
    public float rotatespeed = 80f;
    // 最小移动阈值，用于判断是否进行急停操作  
    public float moveThreshold = 0.1f;
    Vector3 movedirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movedirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movedirection.magnitude > moveThreshold)
        {
            float movex = movedirection.x;
            float rotateangle = movex * rotatespeed * Time.deltaTime;
            transform.Rotate(0, rotateangle, 0);
        }
    }
}

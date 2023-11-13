using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Antiphysical_Move : MonoBehaviour
{
    public float movespeed = 5f;
    Vector3 movedirection = Vector3.zero;
    // 最小移动阈值，用于判断是否进行急停操作  
    public float moveThreshold = 0.1f;
    float originalspeed;
    // Start is called before the first frame update
    void Start()
    {
        originalspeed = movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = 2 * originalspeed; 
        }
        else
        {
            movespeed = originalspeed;
        }
        // 获取按键输入并进行记录  
        movedirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // 通过按键控制角色的移动和转向  
        if (movedirection.magnitude > moveThreshold) // 当玩家有移动输入时  
        {
            // 规范化移动方向向量  
            movedirection = movedirection.normalized;

            // 通过W、S键控制前进和后退  
            float movez = movedirection.z;
            transform.position += transform.forward * movez * movespeed * Time.deltaTime;

            // 通过A、D键控制左右移动  
            float movex = movedirection.x;
            transform.position += transform.right * movex * movespeed * Time.deltaTime;

        }
        else // 当玩家没有进行任何移动操作时不进行急停  
        {
            // 不进行任何操作，保持当前状态  
        }
    }
}

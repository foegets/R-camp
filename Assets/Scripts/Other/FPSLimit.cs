using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimit : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;//锁定最大帧率为165帧
    }
}

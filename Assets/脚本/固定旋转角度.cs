using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 固定旋转角度 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spine();
    }
    void Spine()//金币旋转
    {
        this.transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime, Space.Self);
    }
}

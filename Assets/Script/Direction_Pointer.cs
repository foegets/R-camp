using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 方向指针 : MonoBehaviour
{
    public GameObject targetgo;
    public float Yoffset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetgo.transform.position.x, targetgo.transform.position.y + Yoffset, targetgo.transform.position.z);
        transform.eulerAngles = new Vector3(90, targetgo.transform.eulerAngles.y, targetgo.transform.eulerAngles.z);
    }
}

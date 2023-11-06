using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(target!=null)
        {
            if(transform.position!=target.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
            }
        }
    }
    void Update()
    {
        
    }
}

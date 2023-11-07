using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squaremove : MonoBehaviour
{
    public float speed;
    private Vector3 targetPosition = new Vector3(5, 0, 0);
    private Vector3 refSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref  refSpeed,1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float rotateSpeed;

  
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed *Time.fixedDeltaTime, 0);
    }

}

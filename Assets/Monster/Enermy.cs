using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enermy : MonoBehaviour
{
    //π÷ŒÔ“∆∂Ø
    protected Rigidbody2D rb;
    protected Animator an;
    public float nomalSpeed;
    public float nowSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        nowSpeed = nomalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

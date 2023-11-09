using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontral : MonoBehaviour
{
    public Rigidbody2D rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Jump");
        float h = Input.GetAxis("Horizontal");
        rd.AddForce(new Vector2(h,v*3) );
    }
}

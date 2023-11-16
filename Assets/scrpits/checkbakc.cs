using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkbakc : MonoBehaviour
{
    private Transform mytransform;
    private Rigidbody2D myrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        mytransform=gameObject.GetComponent<Transform>();
        myrigidbody=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("00");
        myrigidbody.AddForce(new Vector2(-30, 4), ForceMode2D.Impulse);
    }
}

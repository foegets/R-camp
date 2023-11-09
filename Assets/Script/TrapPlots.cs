using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlots : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //检测碰撞
        Debug.Log(other.collider.CompareTag("Player"));
        if (other.collider.CompareTag("Player"))
        {
            //当接触到Player时陷阱地块消失
            Destroy(gameObject);
            Debug.Log(other.collider.CompareTag("Player"));
        }
    }
}

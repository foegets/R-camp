using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striction : MonoBehaviour
{
    
    public Rigidbody2D body;


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
           body.gameObject.SetActive(false);


        }//Ҫ��������collision֮�󳹵׸ı��˶�����
    }
}

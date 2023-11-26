using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extiction : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Destroy(collision.gameObject);
        }
    }
}

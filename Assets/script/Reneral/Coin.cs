using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int currentcoin = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collection"))
        {
            Destroy(collision.gameObject);
            currentcoin += 1;
        }
    }
}

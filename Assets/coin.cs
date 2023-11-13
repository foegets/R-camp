using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
     public static int coinCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            coinCount++;
            
        }
    }
    private void OnGUI()
    {
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(10, 10, 300, 300), "coin Num:" + coinCount);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruits : MonoBehaviour
{
    private int Melons = 0;
    [SerializeField] private Text MelonTest; //สตภýปฏ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melon") )
        {
            Destroy(collision.gameObject);
            Melons++;
            MelonTest.text = "Melons" + Melons;
        }
    }
}

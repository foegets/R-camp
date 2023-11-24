using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && 
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            GemUI.currentGemQuantity += 1;
            Destroy(gameObject);
        }
    }
}

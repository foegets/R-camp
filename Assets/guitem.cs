using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guitem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player") &&
            other.GetType().ToString()== "UnityEngine.BoxCollider2D")
        {
            GuUI.guNow += 1;
            Destroy(gameObject);
        }
    }
}

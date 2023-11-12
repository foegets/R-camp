using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerThing : MonoBehaviour
{
    public GameObject Player;
    public GameObject Triangle;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Triangle"))
            transform.position = new Vector3(-8, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        
        
            
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CoinUse1 : MonoBehaviour
{
    public UnityEvent Dragon;
   
    // Start is called before the first frame update
    void Awake()
    {
      
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
       
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))  //以后再开个脚本写可以被主角拾取的硬币
        {
           
            Dragon?.Invoke();
                Destroy(gameObject);
        }
     
    }
}

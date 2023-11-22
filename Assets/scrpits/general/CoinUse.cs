using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUse : MonoBehaviour
{
    
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
        //if(other.gameObject.CompareTag("Player"))  //以后再开个脚本写可以被主角拾取的硬币
        //{
        //    
        //        coinUI.CurrentCoinQuantity += 1;
        //        Destroy(gameObject);
        //}
        if (other.gameObject.CompareTag("OTTO"))
        {
            Destroy(gameObject);
            
        }
    }
}

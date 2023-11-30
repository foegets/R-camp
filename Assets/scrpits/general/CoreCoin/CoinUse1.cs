using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CoinUse1 : MonoBehaviour
{
    public UnityEvent Dragon;
    public float hurttime;
    public float HurtWaittime;
    // Start is called before the first frame update
    private void Awake()
    {
        hurttime = 0.1f;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        hurttime-=Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.CompareTag("Player")&&hurttime<=0)  //以后再开个脚本写可以被主角拾取的硬币
        {
           
            Dragon?.Invoke();
                Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("OTTO"))  
            Destroy(gameObject);



    }
    public void hurt()
    {
        hurttime = HurtWaittime;

    }
}

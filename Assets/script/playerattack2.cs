using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class playerattack2 : MonoBehaviour
{
    
    public float time;
    private Animator animator;
    void Start()
    {
        
    }
    void Update()
    {
  
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
           //Åö×²¼ì²â£¨Íæ¼Ò¹¥»÷Ð¡¹Ö£©
        if (collision.gameObject.CompareTag("Turtle")/*&&collision.gameObject.GetComponent<Turtle>().damageTime<=0*/)
        {
            gameManager.cameraShake.Shake();//ÆÁÄ»Õð¶¯
            print("Turle");
        }
        if (collision.gameObject.CompareTag("Trunk") /*&& collision.gameObject.GetComponent<Trunk>().damageTime <= 0*/)
        {
            gameManager.cameraShake.Shake();//ÆÁÄ»Õð¶¯
            print("trunk");
        }
 
    }
}


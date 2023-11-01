using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerCtrl plr = collision.gameObject.GetComponent<PlayerCtrl>();
            if(plr)
            {
                Vector2 v = plr.rb.velocity;
                v.y +=120;
                plr.rb.velocity = v;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}

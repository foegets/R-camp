using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int health;
    private bool isDead=true;
    public Collider2D cd;
    public Animator anim;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Arrow")
        {
            health--;
        }
        if(health == 0)
        {
            Debug.Log(1);
            anim.SetBool("Dead", isDead);
            
        }
    }
}

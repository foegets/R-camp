using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private bool isgem;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("pick", isgem);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.CompareTag( "Player"))
            {
            isgem = true;
            levelmanager.instance.gemget++;
             Destroy(gameObject,1f);
            UI.instance.UIupdate();
            }
        
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Controler : MonoBehaviour
{
    bool isjumping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isjumping == false && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("jump-up");
            GetComponent<Animator>().SetBool("isjumpdown", false);
            isjumping = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Animator>().SetTrigger("search");
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<Animator>().SetBool("isrightrunning", true);
                    GetComponent<Animator>().SetBool("isfrontrunning", false);
                    GetComponent<Animator>().SetBool("isleftrunning", false);
                    GetComponent<Animator>().SetBool("isfrontwalking", false);
                    GetComponent<Animator>().SetBool("isleftwalking", false);
                    GetComponent<Animator>().SetBool("isrightwalking", false);
                }
                else
                {
                    GetComponent<Animator>().SetBool("isrightwalking", true);
                    GetComponent<Animator>().SetBool("isfrontwalking", false);
                    GetComponent<Animator>().SetBool("isleftwalking", false);
                    GetComponent<Animator>().SetBool("isrightrunning", false);
                    GetComponent<Animator>().SetBool("isfrontrunning", false);
                    GetComponent<Animator>().SetBool("isleftrunning", false);
                }               
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<Animator>().SetBool("isrightrunning", false);
                    GetComponent<Animator>().SetBool("isfrontrunning", false);
                    GetComponent<Animator>().SetBool("isleftrunning", true);
                    GetComponent<Animator>().SetBool("isfrontwalking", false);
                    GetComponent<Animator>().SetBool("isleftwalking", false);
                    GetComponent<Animator>().SetBool("isrightwalking", false);
                }
                else
                {
                    GetComponent<Animator>().SetBool("isleftwalking", true);
                    GetComponent<Animator>().SetBool("isrightwalking", false);
                    GetComponent<Animator>().SetBool("isfrontwalking", false);
                    GetComponent<Animator>().SetBool("isrightrunning", false);
                    GetComponent<Animator>().SetBool("isfrontrunning", false);
                    GetComponent<Animator>().SetBool("isleftrunning", false);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<Animator>().SetBool("isrightrunning", false);
                    GetComponent<Animator>().SetBool("isfrontrunning", true);
                    GetComponent<Animator>().SetBool("isleftrunning", false);
                    GetComponent<Animator>().SetBool("isfrontwalking", false);
                    GetComponent<Animator>().SetBool("isleftwalking", false);
                    GetComponent<Animator>().SetBool("isrightwalking", false);
                }
                else
                {
                    GetComponent<Animator>().SetBool("isfrontwalking", true);
                    GetComponent<Animator>().SetBool("isleftwalking", false);
                    GetComponent<Animator>().SetBool("isrightwalking", false);
                    GetComponent<Animator>().SetBool("isrightrunning", false);
                    GetComponent<Animator>().SetBool("isfrontrunning", false);
                    GetComponent<Animator>().SetBool("isleftrunning", false);
                }               
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isfrontwalking", false);
            GetComponent<Animator>().SetBool("isleftwalking", false);
            GetComponent<Animator>().SetBool("isrightwalking", false);
            GetComponent<Animator>().SetBool("isrightrunning", false);
            GetComponent<Animator>().SetBool("isfrontrunning", false);
            GetComponent<Animator>().SetBool("isleftrunning", false);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GetComponent<Animator>().SetBool("isjumpdown", true);
            isjumping = false;
        }
    }
}

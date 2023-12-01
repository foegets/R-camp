using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Animation_Controler : MonoBehaviour
{
    //// 获取头部要看向的物体
    //public GameObject target;
    //// 获得CharacterController
    //CharacterController playercontroller;
    // 获取动画机组件
    Animator playeranimator;
    bool isjumping = false;
    // Start is called before the first frame update
    void Start()
    {
        //playercontroller = GetComponent<CharacterController>();
        playeranimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GetComponent<Physical_Jump>().isGroundHeight <= 0.1f && isjumping)
        //{
        //    playeranimator.SetBool("isjumpdown", true);
        //    isjumping = false;
        //}

        if (isjumping == false && Input.GetKeyDown(KeyCode.Space))
        {
            playeranimator.SetTrigger("jump-up");
            playeranimator.SetBool("isjumpdown", false);
            isjumping = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            playeranimator.SetTrigger("search");
        }
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playeranimator.SetFloat("NormalMove", 1f);
                    
                }
                else
                {
                    playeranimator.SetFloat("NormalMove", 0f);
                }
                playeranimator.SetBool("isfrontmoving", false);
                playeranimator.SetBool("isrightmoving", true);
                playeranimator.SetBool("isleftmoving", false);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playeranimator.SetFloat("NormalMove", 1f);
                }
                else
                {
                    playeranimator.SetFloat("NormalMove", 0f);
                }
                playeranimator.SetBool("isfrontmoving", false);
                playeranimator.SetBool("isrightmoving", false);
                playeranimator.SetBool("isleftmoving", true);
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playeranimator.SetFloat("NormalMove", 1f);
                }
                else
                {
                    playeranimator.SetFloat("NormalMove", 0f);
                }
                playeranimator.SetBool("isfrontmoving", true);
                playeranimator.SetBool("isrightmoving", false);
                playeranimator.SetBool("isleftmoving", false);
            }
        }
        else
        {
            playeranimator.SetBool("isfrontmoving", false);
            playeranimator.SetBool("isrightmoving", false);
            playeranimator.SetBool("isleftmoving", false);
        }
        //if (target != null && Vector3.Distance(transform.position, target.transform.position) <= 4 && target.GetComponent<VideoPlayer>().isPlaying)
        //{

        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playeranimator.SetBool("isjumpdown", true);
            isjumping = false;
        }
    }
    //void OnAnimatorIK(int layerIndex)
    //{
    //    GetComponent<Animator>().SetLookAtWeight(0.5f);
    //    GetComponent<Animator>().SetLookAtPosition(target.transform.position);
    //}
}

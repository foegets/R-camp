using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Score = 0;
    public GameObject _player;
    float facing=1;
    Rigidbody2D Rb;
    private const int MoveSpeed=3;

    public bool isOnGround = false;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float Speed = Input.GetAxis("Horizontal") * MoveSpeed;
        if(Input.GetKey(KeyCode.A))
        {
            Facing();
            //Debug.Log(Rb.velocity.x);
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            _player.transform.localScale = new Vector3(facing, 1, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Facing();
            //Debug.Log(Rb.velocity.x);
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            _player.transform.localScale = new Vector3(facing, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.W)&&isOnGround==true)
        {
            //Debug.Log(isOnGround);
            Rb.AddForce(new Vector3(0, 1, 0) * 200);
        }



    }

    void Facing()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            facing = 1;
        }
        else if(Input.GetKeyDown(KeyCode.A)) 
        {
            facing = -1;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {;
        if(other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (other.CompareTag("Trap"))
        {
            //Debug.Log(other.tag);
            Destroy(other.gameObject);
            Rb.AddForce(new Vector3(0, 10, 0) * 40);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOnGround=false;
    }

}

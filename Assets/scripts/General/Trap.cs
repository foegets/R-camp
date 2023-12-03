using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float force;
    private Vector2 forceDir;
    // Start is called before the first frame update
    private void Awake()
    {
        forceDir = new Vector2(0,1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            Debug.Log("PlayOnTrap");
            collision.GetComponent<Rigidbody2D>().AddForce(forceDir*force,ForceMode2D.Impulse);
        }
    }
}

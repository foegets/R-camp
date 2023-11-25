using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trigger : MonoBehaviour
{
    public float damage = 10.0f;
    public float hitForce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player_Status_Monitor status = other.GetComponent<Player_Status_Monitor>();
            status.Player_HP.value -= damage;
            
        }
    }

    
}

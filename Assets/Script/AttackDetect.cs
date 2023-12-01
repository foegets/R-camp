using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetect : MonoBehaviour
{
    // …Ë÷√…À∫¶
    public float AttackDamage = 10;
    void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && this.tag == "Enemy")
        {
            other.GetComponent<Player_Status_Monitor>().HP  -= AttackDamage;
        }
        if (other.CompareTag("Enemy") && this.tag == "Player")
        {
            other.GetComponent<Character_BasicStatus>().HP -= AttackDamage;
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player") && this.tag == "Enemy")
    //    {
    //        other.GetComponent<Player_Status_Monitor>().HP -= AttackDamage * Time.deltaTime;
    //    }
    //    if (other.CompareTag("Enemy") && this.tag == "Player")
    //    {
    //        other.GetComponent<Character_BasicStatus>().HP -= AttackDamage * Time.deltaTime;
    //    }
    //}
}

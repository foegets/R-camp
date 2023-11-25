using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int health;
    public static int healthMax = 20;


    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        HealthBar.HealthCurrent = health;
    }
    public void DamagePlayer(int damage){
        health -= damage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<Attribute>().TakeDamage(this);
    }
}

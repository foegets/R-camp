using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float atackRate;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<character>()?.Takedamage(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collection : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GainGun : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent Gun;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Gun?.Invoke();
            Destroy(gameObject);
        }
    }
}

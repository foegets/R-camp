using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLeft : MonoBehaviour
{
    public GameObject left;
    public void fire() 
    {
        Instantiate(left, transform.position, Quaternion.identity);
    }
}

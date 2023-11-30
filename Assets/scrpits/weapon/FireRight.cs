using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRight : MonoBehaviour
{
    public GameObject right;
    public void fire()
    {
        Instantiate(right, transform.position, Quaternion.identity);
    }
}

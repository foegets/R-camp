using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class injuryEffect : MonoBehaviour
{
    private float destoryTime;
    // Start is called before the first frame update
    void Start()
    {
        destoryTime = 1f;
        Destroy(gameObject, destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

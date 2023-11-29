﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemController : MonoBehaviour
{
    public UnityEvent OnBeingGot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            OnBeingGot?.Invoke();
        }
    }
    public void Disappear()
    {
        Destroy(gameObject);
    }
}

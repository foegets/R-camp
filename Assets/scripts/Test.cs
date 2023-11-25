using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnityEvent TestEvent;
    // Start is called before the first frame update
    void Start()
    {
        TestEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void asd()
    {
        Debug.Log("EVENT");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_and_Close : MonoBehaviour
{
    bool isactive;
    // Start is called before the first frame update
    void Start()
    {
        isactive = gameObject.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openandclose()
    {
        gameObject.SetActive(!isactive);
    }
}

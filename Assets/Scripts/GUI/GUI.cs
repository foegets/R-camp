using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    [SerializeField] bool Show = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (!Show) return;
        GUILayout.Label("Lable Lable Lable\nLable Lable Lable\nLable Lable Lable");
    }


    private void OnDisable()
    {
        print("OnDisable");
    }

    private void OnEnable()
    {
        print("OnEnable");
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;  
    public Text gemtext;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        instance=this;
    }
    public void UIupdate()
    {
        Debug.Log("xxx");
        gemtext.text = levelmanager.instance.gemget.ToString();
    }
}

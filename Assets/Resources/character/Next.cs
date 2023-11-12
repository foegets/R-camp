using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Next : MonoBehaviour
{
  
    // Start is called before the first frame update
    public void NextMenu()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
    
    // Update is called once per frame
  
}

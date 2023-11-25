using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            if (Time.timeScale==1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                Time.timeScale = 0;
            }
           else if(Time.timeScale==0) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Time.timeScale = 1;
            }
        }
    }
}
     
 

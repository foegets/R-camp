using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    static gameManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("2");
    }

    public static void GameOver(bool isdead)
    {
        Debug.Log("3");
        if(isdead)
        {
            Time.timeScale = 0f;
        }
    }
}

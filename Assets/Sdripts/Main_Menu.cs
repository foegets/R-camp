using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public string StartScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Begin_Game()
    {
        SceneManager.LoadScene(StartScene);
        Time.timeScale = 1f;
        LevelManeger.gensCollected = 0;
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting Game");
    }
}

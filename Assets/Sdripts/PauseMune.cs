using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMune : MonoBehaviour
{
    public static PauseMune instance;
    public string  mainMune;
    public GameObject pauseScreen;
    public GameObject genScreen;
    public bool isPaused;
    public bool isGen;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            genUnpause();
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            genUnpause();
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void MainMune()
    {
        genUnpause();
        SceneManager.LoadScene(mainMune);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void genUnpause()
    {
        if (isGen)
        {
            isGen = false;
            genScreen.SetActive(false);
        }
        else
        {
            isGen = true;
            genScreen.SetActive(true);
        }
    }
}

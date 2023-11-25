using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    public static Spikes instance;
    public string mainMune,again;
    public GameObject pauseScreen;
    public  bool isDied;
    private PauseMune PauseMune;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PauseMune = PauseMune.instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isDied = true;
            DIE();
            Time.timeScale = 0f;
        }
        


    }

    public void DIE()
    {
        
        pauseScreen.SetActive(true);
        PauseMune.instance.isPaused = true;
        
    }
     public void MainMune()
    {
        SceneManager.LoadScene(mainMune);
        isDied = false;
        PauseMune.instance.isPaused = false;
        pauseScreen.SetActive(false);
    }

    public void Again()
    {
        SceneManager.LoadScene(again);
        isDied = false;
        PauseMune.instance.isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        LevelManeger.gensCollected = 0;
    }

}

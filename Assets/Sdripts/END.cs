using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Effect : MonoBehaviour
{
    //public bool isEffect;
   // public GameObject prefaEffect;

    public string mainMune,Again;
    public GameObject VScreen;

    private PauseMune PauseMune;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseMune = PauseMune.instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        VUnpause();
    }
    public void VUnpause()//结束UI
    {
        VScreen.SetActive(true);
        Time.timeScale = 0f;
        PauseMune.isPaused = true;
    }

    public void Again_Game()//重新开始
    {
        SceneManager.LoadScene(Again);
        Time.timeScale = 1f;
        PauseMune.isPaused = false;
        LevelManeger.gensCollected=0;
    }
    public void MainMune()//主菜单
    {
        VScreen.SetActive(false);
        SceneManager.LoadScene(mainMune);
        PauseMune.isPaused = false;
        Time.timeScale = 1f;
    }
}

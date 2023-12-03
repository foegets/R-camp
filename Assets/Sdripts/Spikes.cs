using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    public static Spikes instance;//实例化1

    private PauseMune PauseMune;//引用实例

    public string mainMune,again;//场景
    public GameObject pauseScreen;//死亡UI

    public  bool isDied;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PauseMune = PauseMune.instance;//实例化2
    }

    public void OnTriggerEnter2D(Collider2D other)//碰撞判定
    {
        if (other.CompareTag("player"))
        {
            isDied = true;//死亡
            DIE();
            Time.timeScale = 0f;
        }
        


    }

    public void DIE()
    {
        MusicManeger.instance.PlaySound(3);//死亡音效

        pauseScreen.SetActive(true);
        PauseMune.instance.isPaused = true;
    }
     public void MainMune()//返回主菜单
    {
        SceneManager.LoadScene(mainMune);
        isDied = false;
        PauseMune.instance.isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Again()//重新开始
    {
        SceneManager.LoadScene(again);
        isDied = false;
        PauseMune.instance.isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        LevelManeger.gensCollected = 0;
    }

}

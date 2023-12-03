using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    public static Spikes instance;//ʵ����1

    private PauseMune PauseMune;//����ʵ��

    public string mainMune,again;//����
    public GameObject pauseScreen;//����UI

    public  bool isDied;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PauseMune = PauseMune.instance;//ʵ����2
    }

    public void OnTriggerEnter2D(Collider2D other)//��ײ�ж�
    {
        if (other.CompareTag("player"))
        {
            isDied = true;//����
            DIE();
            Time.timeScale = 0f;
        }
        


    }

    public void DIE()
    {
        MusicManeger.instance.PlaySound(3);//������Ч

        pauseScreen.SetActive(true);
        PauseMune.instance.isPaused = true;
    }
     public void MainMune()//�������˵�
    {
        SceneManager.LoadScene(mainMune);
        isDied = false;
        PauseMune.instance.isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Again()//���¿�ʼ
    {
        SceneManager.LoadScene(again);
        isDied = false;
        PauseMune.instance.isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        LevelManeger.gensCollected = 0;
    }

}

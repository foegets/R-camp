using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);//按下START会加载第一个场景开始
    }
    public void ExitGame()
    {
        Application.Quit();//按下QUIT会退出游戏（调试下无反应，打包后才能退出）
    }
}

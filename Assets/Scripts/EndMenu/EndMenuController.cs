using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public void ReStartGame()
    {
        SceneManager.LoadSceneAsync(1);//按下RESTART重新加载第一个场景开始
    }
    public void ExitGame()
    {
        Application.Quit();//按下QUIT会退出游戏
    }
}

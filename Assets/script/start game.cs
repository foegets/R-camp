using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startgame : MonoBehaviour
{   
           
    public void OnClick()
    {
        // ��ť���ʱִ�еĲ���
        
        LoadScene("main");
    }
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Scene scene = SceneManager.GetSceneByName(sceneName);
        //SceneManager.SetActiveScene(scene);
    }
}

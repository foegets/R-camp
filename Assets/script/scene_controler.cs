using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_controler : MonoBehaviour
{   
    private void Start()
    {
        // 获取当前场景的信息
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        UnloadScene("main");
        if (sceneName != "ui") { 
        LoadScene("ui");     
        }
        
        
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }

    void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
     
}
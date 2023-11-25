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
        //UnloadScene("main");
        if (sceneName != "ui") { 
        LoadScene("ui");     
        }
        
        
    }
    public void LoadScene(string sceneName)//切换
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }

    void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    public string sceneName;
    //异步加载对象
    private AsyncOperation asyncLoad;

    
    IEnumerator LoadScene1(string sceneName)
    {
        //获取加载对象
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        //设置加载完成后不跳转
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            //输出加载进度
            Debug.Log(asyncLoad.progress);
            //进度.百分之九十后进行操作,当进度为百分之90其实已经完成了大部分的工作,就可以进行下面的逻辑处理了
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startgame : MonoBehaviour
{
    public string sceneName;
    //异步加载对象
    private AsyncOperation asyncLoad;
    public void OnClick()
    {
        // 按钮点击时执行的操作

        StartCoroutine(LoadScene1("main"));
    }
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Scene scene = SceneManager.GetSceneByName(sceneName);
        //SceneManager.SetActiveScene(scene);
    }
    IEnumerator LoadScene1(string sceneName)
    {
        //获取加载对象
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single );
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

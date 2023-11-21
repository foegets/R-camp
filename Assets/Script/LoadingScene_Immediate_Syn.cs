using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene_Immediate_Syn : MonoBehaviour
{
    // 获取要加载的场景的下标
    public int SceneIndex;
    // 获取异步加载的场景对象
    AsyncOperation asyncLoad;
    // 获取加载进度
    float loadingprogress;
    // 获得Slider的组件
    Slider loadprogrssbar;
    // Start is called before the first frame update
    void Start()
    {
        loadingprogress = 0f;
        loadprogrssbar = GetComponent<Slider>();
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(SceneIndex);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            loadingprogress = asyncLoad.progress;
            loadprogrssbar.value = loadingprogress;
            if (asyncLoad.progress >= 0.9f)
            {
                loadprogrssbar.value = 1f;
                yield return new WaitForSeconds(1.5f);
                asyncLoad.allowSceneActivation = true;
                // 销毁原场景
                SceneManager.UnloadSceneAsync(1);
            }
            
        }
    }
}

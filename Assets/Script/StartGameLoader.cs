using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameLoader : MonoBehaviour
{
    public bool isTrigger;
    public float ElapedTime;
    //// 异步加载对象
    //AsyncOperation asyncLoad;
    // Start is called before the first frame update
    void Start()
    {
        isTrigger = false;
        ElapedTime = 0;
        //StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        ElapedTime = GetComponent<MenuButtonMonitor>().ElapedTime;
        isTrigger = GetComponent<MenuButtonMonitor>().isTrigger;
        if (isTrigger && ElapedTime >= 2.2f)
        {
            //asyncLoad.allowSceneActivation = true;
            //SceneManager.UnloadSceneAsync(0);

            SceneManager.LoadScene(1);
        }

    }
    //IEnumerator LoadScene()
    //{
    //    asyncLoad = SceneManager.LoadSceneAsync(1);// 跳转到Loading场景
    //    asyncLoad.allowSceneActivation = false;
        
    //    yield return null;
    //}
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameLoader : MonoBehaviour
{
    public bool isTrigger;
    public float ElapedTime;
    //// �첽���ض���
    //AsyncOperation asyncLoad;
    void Start()
    {
        isTrigger = false;
        ElapedTime = 0;
        //StartCoroutine(LoadScene());
    }

    void Update()
    {
        ElapedTime = GetComponent<MenuButtonMonitor>().ElapedTime;
        isTrigger = GetComponent<MenuButtonMonitor>().isTrigger;
        if (isTrigger && ElapedTime >= 2.2f)
        {
            GameManager.Instance.NextSceneIndex = 2;
            //asyncLoad.allowSceneActivation = true;
            //SceneManager.UnloadSceneAsync(0);

            SceneManager.LoadScene(1);
        }

    }
    //IEnumerator LoadScene()
    //{
    //    asyncLoad = SceneManager.LoadSceneAsync(1);// ��ת��Loading����
    //    asyncLoad.allowSceneActivation = false;

    //    yield return null;
    //}
}

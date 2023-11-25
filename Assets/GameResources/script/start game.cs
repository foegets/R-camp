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
    //�첽���ض���
    private AsyncOperation asyncLoad;
    public void OnClick()
    {
        // ��ť���ʱִ�еĲ���

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
        //��ȡ���ض���
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single );
        //���ü�����ɺ���ת
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            //������ؽ���
            Debug.Log(asyncLoad.progress);
            //����.�ٷ�֮��ʮ����в���,������Ϊ�ٷ�֮90��ʵ�Ѿ�����˴󲿷ֵĹ���,�Ϳ��Խ���������߼�������
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

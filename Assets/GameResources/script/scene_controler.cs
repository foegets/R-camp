using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_controler : MonoBehaviour
{   
    private void Start()
    {
        // ��ȡ��ǰ��������Ϣ
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        //UnloadScene("main");
        if (sceneName != "ui") { 
        LoadScene("ui");     
        }
        
        
    }
    public void LoadScene(string sceneName)//�л�
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
    //�첽���ض���
    private AsyncOperation asyncLoad;

    
    IEnumerator LoadScene1(string sceneName)
    {
        //��ȡ���ض���
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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
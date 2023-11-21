using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene_Immediate_Syn : MonoBehaviour
{
    // ��ȡҪ���صĳ������±�
    public int SceneIndex;
    // ��ȡ�첽���صĳ�������
    AsyncOperation asyncLoad;
    // ��ȡ���ؽ���
    float loadingprogress;
    // ���Slider�����
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
                // ����ԭ����
                SceneManager.UnloadSceneAsync(1);
            }
            
        }
    }
}

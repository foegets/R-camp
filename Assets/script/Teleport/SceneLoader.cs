using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//�ýű�����ͳ��ʵ�ֳ���ת��
public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    [Header("�¼�����")]
    public SceneLoadEventSO LoadEventSO;
    public GameSceneSO firstLoadScene;
    public GameSceneSO secondLoadScene;
    private GameSceneSO curruntLoadedScene;
    //�������б���������ʱ�����¼��������Ĳ�����Ϊ���������л��ĵȴ�ʱ��
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;

    public float fadeDuration;
    private bool isLoading;//���ڼ��س��������йرճ������ص��¼�����ֹ���Ƶ����E���س���
   
    private void Awake()
    {
        //�첽���س���
        curruntLoadedScene = firstLoadScene;
        curruntLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    //ע���¼�
    private void OnEnable()
    {
        LoadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    //ע���¼�
    private void OnDisable()
    {
        LoadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

    //���ô������Ĳ����������³���������
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if(isLoading)
        {
            return;
        }
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;//this���ڵ�������ͬ����ʱָ���ýű��ڴ����ı�����
        StartCoroutine(UnLoadPreviousScene());
    }
    
    //����һ��Я�̵ķ���,ж�ؾɵĵ�ǰ�ĳ���
    private IEnumerator UnLoadPreviousScene()
    {
        if(fadeScreen)
        {
            //ʵ�ֵ��뵭��
        }
        //����������ж�س�����ͬʱ�ر�����
        yield return new WaitForSeconds(fadeDuration);
        if(curruntLoadedScene != null)
        {
            yield return curruntLoadedScene.sceneReference.UnLoadScene();
        }
        playerTrans.gameObject.SetActive(false);
        //�����³���
        LoadNewScene();
    }
    //�³������ط���
    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;//�����ñ�����completed��������ɺ�ִ��һ������
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        curruntLoadedScene = sceneToLoad;//������ɺ�ı䵱ǰ��currunLoadScene
        playerTrans.position = positionToGo;//�����������������ǰpersistent��player
        playerTrans.gameObject.SetActive(true);//��������
        if (fadeScreen)
        {

        }
        isLoading = false;
    }
}

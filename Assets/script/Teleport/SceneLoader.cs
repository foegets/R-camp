using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//该脚本用于统筹实现场景转换
public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    [Header("事件监听")]
    public SceneLoadEventSO LoadEventSO;
    public GameSceneSO firstLoadScene;
    public GameSceneSO secondLoadScene;
    private GameSceneSO curruntLoadedScene;
    //下面三行变量用于暂时储存事件传进来的参数，为了做场景切换的等待时间
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;

    public float fadeDuration;
    private bool isLoading;//用于加载场景过程中关闭场景加载的事件，防止玩家频繁按E加载场景
   
    private void Awake()
    {
        //异步加载场景
        curruntLoadedScene = firstLoadScene;
        curruntLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    //注册事件
    private void OnEnable()
    {
        LoadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }

    //注销事件
    private void OnDisable()
    {
        LoadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

    //调用传进来的参数，加载新场景和坐标
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if(isLoading)
        {
            return;
        }
        isLoading = true;
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;//this用于当出现相同名字时指代该脚本内创建的变量？
        StartCoroutine(UnLoadPreviousScene());
    }
    
    //创建一个携程的方法,卸载旧的当前的场景
    private IEnumerator UnLoadPreviousScene()
    {
        if(fadeScreen)
        {
            //实现淡入淡出
        }
        //淡出结束，卸载场景，同时关闭人物
        yield return new WaitForSeconds(fadeDuration);
        if(curruntLoadedScene != null)
        {
            yield return curruntLoadedScene.sceneReference.UnLoadScene();
        }
        playerTrans.gameObject.SetActive(false);
        //加载新场景
        LoadNewScene();
    }
    //新场景加载方法
    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;//创建该变量，completed即加载完成后执行一个方法
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        curruntLoadedScene = sceneToLoad;//加载完成后改变当前的currunLoadScene
        playerTrans.position = positionToGo;//将传进来的坐标给当前persistent的player
        playerTrans.gameObject.SetActive(true);//启用人物
        if (fadeScreen)
        {

        }
        isLoading = false;
    }
}

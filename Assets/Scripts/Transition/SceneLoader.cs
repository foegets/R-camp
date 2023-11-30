using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //传入玩家坐标
    public Transform playerTrans;
    //玩家第一个要去的坐标
    public Vector3 firstPosition;
    //玩家在菜单栏的时候应该在的坐标
    public Vector3 menuPosition;

    [Header("事件监听")]
    //要加载场景力
    public SceneLoadEventSO loadEventSO;
    //哦呦，新的冒险要开始了吗！
    public VoidEventSO newGameEvent;


    [Header("广播")]
    //我加载完场景啦！
    public VoidEventSO afterSceneLoadedEvent;
    //我要渐入渐出啦！
    public FadeEventSO fadeEvent;
    //卸载完上一个场景力！
    public SceneLoadEventSO unloadedSceneEvent;

    [Header("场景")]
    //获取第一个要加载的场景
    public GameSceneSO firstLoadScene;
    //获取主菜单场景捏
    public GameSceneSO menuScene;

    //获取当前加载的场景(ser序列化，使得该变量能在unity窗口显示)
    [SerializeField]private GameSceneSO currentLoadedScene;

    //临时存储传进来的变量
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    //判断是不是正在加载场景
    private bool isLoading;

    //渐入渐出，加载时间
    public float fadeDuration;

    //太早了，其他代码还没生成（？？？(awake在onenable之前就执行了）
    public void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadedScene = firstLoadScene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);

    }

    //勇士传说，启动！
    private void Start()
    {
        loadEventSO.RaiseLoadRequestEvent(menuScene, menuPosition, true);
        //NewGame();
    }

    //注册事件
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRaised += NewGame;
    }

    //注销事件
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRaised -= NewGame;
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        //OnLoadRequestEvent(sceneToLoad, firstPosition, true);
        //自己呼叫自己喵？？？？
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    //事件
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        //如果正在加载的话，约束玩家不能再按E了，不然会出大问题
        if(isLoading){
            return;
        }
        //切换为正在加载的状态
        isLoading = true;
        //赋值
        sceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;

        /*确保事件能正常被唤醒
        Debug.Log(sceneToLoad.sceneReference.SubObjectName);*/

        //当前场景存在，就启动携程
        if(currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }else
        {
            LoadNewScene();
        }
        
    }

    //创建一个携程方法(涉及计算，等待一定事件再进行（？)
    private IEnumerator UnLoadPreviousScene()
    {
        //变黑！
        if(fadeScreen){
            fadeEvent.FadeIn(fadeDuration);
        }

        //等到场景变黑(一段时间)之后才执行下一步（即卸载场景
        yield return new WaitForSeconds(fadeDuration);

        //广播事件调整血条显示
        unloadedSceneEvent.RaiseLoadRequestEvent(sceneToLoad,positionToGo,true);

        //等待卸载完毕之后才执行下一步
        yield return currentLoadedScene.sceneReference.UnLoadScene();

        //场景卸载之后就关闭人物
        playerTrans.gameObject.SetActive(false);
        //加载新场景
        LoadNewScene();
    }

    //加载新场景
    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        //场景加载结束后执行这个函数捏
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        //切换当前场景
        currentLoadedScene = sceneToLoad;
        //告诉玩家下个图要去哪个地方
        playerTrans.position = positionToGo;

        //登登！玩家闪亮登场！
        playerTrans.gameObject.SetActive(true);

        //变透明！
        if(fadeScreen){
            fadeEvent.FadeOut(fadeDuration);
        }

        //切换状态为加载完毕！
        isLoading = false;

        //（如果当前不是菜单界面滴话）广播一下！！告诉他们，我的场景加载完啦！！可以移动啦！
        if(currentLoadedScene.sceneType != SceneType.Menu)
            afterSceneLoadedEvent.RaiseEvent();
    }


}

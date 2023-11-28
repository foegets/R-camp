using UnityEngine;
//记得调用addressable的命名空间嗷
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]

public class GameSceneSO : ScriptableObject
{
    //创建这个枚举变量
    public SceneType sceneType;
    //用到这个场景的引用捏
    public AssetReference sceneReference;

    
}


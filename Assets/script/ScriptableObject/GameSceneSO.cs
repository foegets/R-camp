using System;
using TMPro;
using UnityEditor.AddressableAssets.Build.Layout;
using UnityEngine;
using UnityEngine.AddressableAssets;

//该脚本用于创建场景给addressable
[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    //加载asset的引用
    public AssetReference sceneReference;
    public AssetType sceneType;
}

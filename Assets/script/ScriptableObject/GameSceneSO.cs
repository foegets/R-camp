using System;
using TMPro;
using UnityEditor.AddressableAssets.Build.Layout;
using UnityEngine;
using UnityEngine.AddressableAssets;

//�ýű����ڴ���������addressable
[CreateAssetMenu(menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    //����asset������
    public AssetReference sceneReference;
    public AssetType sceneType;
}

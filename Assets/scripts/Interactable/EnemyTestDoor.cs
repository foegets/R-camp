using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class EnemyTestDoor : MonoBehaviour , IInteractable
{
    public GameScenceSO scenceToGo;
    public GameScenceSO currentLoadedScene;
    public Vector3 positionToGo;
       public void TriggerAction()
    {
            if(currentLoadedScene != null)
        {
            currentLoadedScene.scenceReference.UnLoadScene();
        }
         scenceToGo.scenceReference.LoadSceneAsync ( LoadSceneMode.Additive,true );
    }
    
}

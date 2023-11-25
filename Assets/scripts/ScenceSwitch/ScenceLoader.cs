using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
public class ScenceLoader : MonoBehaviour
{
    public GameScenceSO firstLoadScence;
    public ScenceLoadEventSO loadEventSO;
    public float fadeDurition;
    private GameScenceSO currentLoadedScence;
    private GameScenceSO scenceToLoad;
    private Vector3 positonToGo;
    private bool fadeScreen;
    private void Awake()
    {
        Addressables.LoadSceneAsync(firstLoadScence.scenceReference , LoadSceneMode.Additive);
        currentLoadedScence = firstLoadScence;
    }
    private void OnEnable()
    {
    
    }
    private void OnDisable()
    {
        
    }
    private IEnumerator UnLoadPreviousScence()
    {
        if (fadeScreen)
        {

        }
        yield return new WaitForSeconds(fadeDurition); 
    }
}

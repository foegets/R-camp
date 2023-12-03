using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinCount : MonoBehaviour
{
    public int CoinCount=0;
    
    
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO sceneToGo;
    public Vector3 positionToGo = new Vector3(25.02F, 0.3F, 0);
    public GameObject canvas;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldCoin"))
        {
            CoinCount++;
        }

        if (CoinCount >= 10)
        {
            //canvas.SetActive(false);
           // loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
           // player.SetActive(false);
        }
    }

}

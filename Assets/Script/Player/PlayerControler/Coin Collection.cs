using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollection : MonoBehaviour
{
    int coin;
    public static int coinCount;
    public Text coinText;
    void Start()
    {
        coin = LayerMask.NameToLayer("Coin");
    }
    // Update is called once per frame
    public void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (other.gameObject.layer == coin)
        {
            coinCount++;
            UpdateCoinText();
            other.gameObject.SetActive(false);
            AudioManger.Instance.PlaySFX("PickUp");
        }
    }
    private void UpdateCoinText()
    {

        coinText.text = "Coins: " + coinCount;

    }
}

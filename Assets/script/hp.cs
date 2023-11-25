using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    private Image ImageHp;
    public Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        ImageHp = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        ImageHp.fillAmount = player.playerHealth / player.maxPlayerHp;
        hpText.text = player.playerHealth + "/" + player.maxPlayerHp;
        if(player.playerHealth<0)//小于0时让text失活
        {
            hpText.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinUI : MonoBehaviour
{
    public int startCoinQuantity;
    public Text coinQuanity;
    public static int CurrentCoinQuantity;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCoinQuantity = startCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuanity.text=CurrentCoinQuantity.ToString();
    }
    public void dead()
    {
        CurrentCoinQuantity = 0;

    }
}

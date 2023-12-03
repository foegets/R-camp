using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Text genText;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdataGen();
    }

    public void UpdataGen()
    {
        genText.text =LevelManeger.gensCollected.ToString();
    }

}

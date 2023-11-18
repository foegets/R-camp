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
    
    public static int gensCollected;
    //public static int gensCollected;
    // Start is called before the first frame update
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

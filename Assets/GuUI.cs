using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuUI : MonoBehaviour
{
    public int startGuNum;
    public Text guNum;

    public static int guNow;

    void Start()
    {
        guNow = startGuNum;
    }

    // Update is called once per frame
    void Update()
    {
        guNum.text = guNow.ToString();
    }
}

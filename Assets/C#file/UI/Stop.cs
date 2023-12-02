using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stop : MonoBehaviour
{
    public GameObject start;
    public GameObject Genshin;
    public void StopButton()
    {
        Time.timeScale = 0;
        start.SetActive(true);
        Genshin.SetActive(true);
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        start.SetActive(false);
        Genshin.SetActive(false);
    }
}

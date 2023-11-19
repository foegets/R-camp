using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    public GameObject start;
    public void StopButton()
    {
        Time.timeScale = 0;
        start.SetActive(true);
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        start.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamecontroller : MonoBehaviour
{


    public int totalScore;
    public TextMeshProUGUI ScoreText;

    public GameObject GameoverPanel;

    public static gamecontroller instance;

    public static object Instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void UpdateTotalScore()
    {
        this.ScoreText.text = totalScore.ToString();
    }
    public void ShowGameoverPanel()
    {
        GameoverPanel.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text scoreText;
    private Score score;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        score = GameObject.Find("player").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + score.totalScore;
    }
}

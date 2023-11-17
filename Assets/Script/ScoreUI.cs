using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text itemsText;
    public static int itemScore;

    // Start is called before the first frame update
    void Start()
    {
        itemScore = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        itemsText.text = itemScore.ToString();
    }
}

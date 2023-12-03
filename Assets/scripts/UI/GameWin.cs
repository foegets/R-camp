using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    private Score score;
    // Start is called before the first frame update
    private void Awake()
    {
        score = GameObject.Find("player").GetComponent<Score>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Win();
    }

    private void Win()
    {
        if (score.totalScore >= 300)
            SceneManager.LoadSceneAsync("Scenes/GameWin", LoadSceneMode.Single);
    }
}

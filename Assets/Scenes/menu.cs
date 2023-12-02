using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //start
    public void GameStart()
    {
        SceneManager.LoadScene(2);
    }



    //continue




    //exit
    public void GameExit()
    {
        Application.Quit();
    }

}

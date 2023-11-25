using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class muneBotton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
   public  void quitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

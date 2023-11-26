using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveManager : MonoBehaviour
{
    public static saveManager instance;
    public amiyaContoller player;
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        instance = this; 
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isCompleteTarget())
        {
            SceneManager.LoadScene(0);
        }
    }
}

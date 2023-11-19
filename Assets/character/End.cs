using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class End : MonoBehaviour
{   
    public Text n;
    public GameObject noEnough;
    // Start is called before the first frame update
    public void NextScene()
    {
        SceneManager.LoadScene("End");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&n.text=="9")
        {
            NextScene();
        }
        else if(collision.CompareTag("Player") && n.text != "9")
        {
            noEnough.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            noEnough.SetActive(false);
        }
    }
}

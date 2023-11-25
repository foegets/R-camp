using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{
    public coinUi coinUi;
    public GameObject isWintest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(coinUi.currentCoinNum>=20 && collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
        else//½ð±Ò²»¹»
        {
            isWintest.SetActive(true);
            Invoke("disableText", 1.5f);
        }
    }
    private void disableText()
    {
        isWintest.SetActive(false);
    }
}

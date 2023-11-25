using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadsreen;
    public Slider slider;
    public Text text;
    public bool isdead;
    public float time;
    public int limit;

     public void Dead()
     {
        isdead = true;
     }

    
    public void FixedUpdate()
    {
        if(isdead==true)
            time-=Time.deltaTime;
        if(time< 0 &&limit==1)
            StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() 
    {
        limit = 0;
        loadsreen.SetActive(true);
        AsyncOperation operation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            slider.value = operation.progress;
            text.text = operation.progress * 100 + "%";
            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
                text.text = "Press Anykey";

                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}

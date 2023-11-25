using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene1Finish : MonoBehaviour
{
    // Start is called before the first frame update
    public float Finishtime;
    public bool Finish;


    public void Update()
    {
        if(Finish==true)
            Finishtime-=Time.deltaTime;

        finish1();
    }
    public void FixedUpdate()
    {
        if (Finish == true)
            Finishtime -= Time.deltaTime;

        finish1();
    }
    public void finish()
    {

        Finish = true;
    }
    public void finish1()
    {
        if (Finishtime <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

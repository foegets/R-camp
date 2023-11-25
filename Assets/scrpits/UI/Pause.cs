using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    // Start is called before the first frame update
    public void pause()
    {
        Time.timeScale = 0;
    }
}

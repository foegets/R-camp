using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    public void onExitGame()
    {
#if UNITY_EDITOR    //UnityÉÏµÄÍË³ö£¿
UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

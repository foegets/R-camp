using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    public void onExitGame()
    {
#if UNITY_EDITOR    //Unity�ϵ��˳���
UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

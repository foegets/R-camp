using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status_Monitor : MonoBehaviour
{
    public Slider Player_HP;
    // Start is called before the first frame update
    void Start()
    {
        Player_HP.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_HP.value == 0)
        {
            gameObject.SetActive(false);
        }
    }
}

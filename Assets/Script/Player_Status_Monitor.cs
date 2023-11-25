using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player_Status_Monitor : MonoBehaviour
{
    public Slider Player_HP;
    public Slider Player_Engry;
    public VideoPlayer videoplayer;
    public GameObject deadimage;
    public Transform OriPos;
    // Start is called before the first frame update
    void Start()
    {
        OriPos = transform;
        Player_HP.value = 100;
        videoplayer.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = OriPos.position;
            transform.rotation = OriPos.rotation;
        }
        if (Player_Engry.value == 5)
        {
            Player_HP.value += 10;
            videoplayer.Play();
            Player_Engry.value = 0;
        }
        if (Player_HP.value == 0)
        {
            deadimage.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

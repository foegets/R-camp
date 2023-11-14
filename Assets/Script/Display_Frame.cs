using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Display_Frame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // »ñÈ¡ÏÔÊ¾¿ò
    public GameObject display_frame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > Screen.width / 2)
        {
            if (Input.mousePosition.y > Screen.height / 2)
            {
                display_frame.transform.position = Input.mousePosition + new Vector3(-50, -15, 0);
            }
        }
        else if (Input.mousePosition.x < Screen.width / 2)
        {
            if (Input.mousePosition.y > Screen.height / 2)
            {
                display_frame.transform.position = Input.mousePosition + new Vector3(50, -15, 0);
            }
        }
        else if(Input.mousePosition.x < Screen.width / 2)
        {
            if (Input.mousePosition.y < Screen.height / 2)
            {
                display_frame.transform.position = Input.mousePosition + new Vector3(50, 15, 0);
            }
        }
        else if(Input.mousePosition.x > Screen.width / 2)
        {
            if (Input.mousePosition.y < Screen.height / 2)
            {
                display_frame.transform.position = Input.mousePosition + new Vector3(-50, -15, 0);
            }
        }
        else
        {
            display_frame.transform.position = Input.mousePosition + new Vector3(50, -15, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        display_frame.SetActive(true);
        display_frame.GetComponentInChildren<TextMeshProUGUI>().text = gameObject.name;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        display_frame.SetActive(false);
        display_frame.GetComponent<TextMeshProUGUI>().text = null;
    }
}

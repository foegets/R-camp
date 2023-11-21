using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recordUI : MonoBehaviour
{
    public int startRecord;
    private int currentRecord;
    public Text recordText;

    void Start()
    {
        currentRecord = startRecord;
    }

    void Update()
    {
       recordText.text = currentRecord.ToString();
    }
}

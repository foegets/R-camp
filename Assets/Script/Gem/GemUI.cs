using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemUI : MonoBehaviour
{
    public int startGemQuantity;
    public Text gemQuantity;

    public static int currentGemQuantity;
    // Start is called before the first frame update
    void Start()
    {
        currentGemQuantity = startGemQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        gemQuantity.text = currentGemQuantity.ToString();
    }
}

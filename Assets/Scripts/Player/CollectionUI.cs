using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUI : MonoBehaviour
{
    public int startCollectionQuantity;
    public TextMeshProUGUI collectionQuantity;//TMP_Text

    public static int CurrentCollectionQuantity;

    // Start is called before the first frame update
    void Start()
    {
        CurrentCollectionQuantity = startCollectionQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        collectionQuantity.text = CurrentCollectionQuantity.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collection : MonoBehaviour
{   
    public scoreCounter t;
    public Text score;

    private void OnTriggerStay2D(Collider2D other)
    {
        //计分
        t.amount ++;
        score.text = t.amount.ToString();

        //物体消失
        Destroy(this.gameObject);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DOOR : MonoBehaviour
{
    public UnityEvent a;//���loadscene�¼�
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        a?.Invoke();
    }
}

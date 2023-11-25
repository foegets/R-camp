using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsMouse : MonoBehaviour
{
    private Vector2 mousePos;
    private Vector2 direction;
    public GameObject Player;
    private Transform tf;
    void Update(){
        transform.localScale = new  Vector3(Player.transform.localScale.x * 0.7612154f,Player.transform.localScale.y * 0.4943593f,0f);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos ).normalized;
        transform.right = direction;
    } 
}

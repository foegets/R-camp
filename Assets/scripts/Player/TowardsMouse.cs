using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsMouse : MonoBehaviour
{
    private Vector2 mousePos;
    private Vector2 direction;
    private Transform tf;
    void Update(){
        transform.localScale = new  Vector3(transform.parent.localScale.x * 0.7612154f,transform.parent.localScale.y * 0.4943593f,0f);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - new Vector2(transform.position.x,transform.position.y)).normalized;
        transform.right = direction;
    } 
}

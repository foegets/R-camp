using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SamuraiTrigger : MonoBehaviour
{
    public GameObject samurai;
    public UnityEvent EnterBossBattle;
    public void Awake()
    {
       // samurai = GetComponent<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // EnterBossBattle?.Invoke();
            samurai.GetComponent<Samurai_FSM>().TransitionState(StateType_Samurai.Enter);
            gameObject.SetActive(false);
        }
    }
}

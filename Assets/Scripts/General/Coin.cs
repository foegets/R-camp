using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour,IInteractable
{
    //确保金币捡过了就不能再捡了
    public bool isPick;

    public UnityEvent OnPickCoin;

    public void TriggerAction()
    {
        Debug.Log("Pick Coin!");
        //如果宝箱没有被打开，就执行打开的函数方法
        if(!isPick)
        {
            PickCoin();
        }
    }

    private void PickCoin()
    {
        OnPickCoin?.Invoke();
        Destroy(this.gameObject);
    }


}

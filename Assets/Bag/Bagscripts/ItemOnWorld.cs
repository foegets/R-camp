using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Bag playerBag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }
    public void AddNewItem()
    {
        if(!playerBag.itemList.Contains(thisItem))
        {
            playerBag.itemList.Add(thisItem);
            //BagManager.CreateNewItem(thisItem);
        }
        thisItem.itemHeld += 1;
        BagManager.RefreshItem();
    }
}

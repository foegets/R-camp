using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{

    [SerializeField] string id;
    public ItemManager.ItemRegistry registry { get => ItemManager.Instance.GetItemRegistry(id); }
    public string Id { get => id; }
    public int MaxStorage { get => registry.maxStorage; }
    public string Name { get => registry.name; }
    public string Description { get => registry.description; }

    // Start is called before the first frame update
}

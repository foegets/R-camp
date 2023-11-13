using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [SerializeField] ItemRegistry[] registries;  //外部编辑物品的注册信息
    public static ItemManager Instance;
    Dictionary<string, ItemRegistry> itemRegistries = new Dictionary<string, ItemRegistry>();  //注册表

    [Serializable]
    public class ItemRegistry
    {
        public string id;
        public string name;
        public string description;
        public int maxStorage = 8; //一格能最多能放几个

        public GameObject Prefab;  //物品实例的预制体
        public Sprite ImgOnGUI;    //物品在物品栏里面的显示图片
    }


    private void Awake()
    {
        Instance = this;
        RegisterAllItems();  //注册物品
    }

    void RegisterAllItems()
    {
        for (int i = 0; i < registries.Length; i++)
        {
            ItemRegistry item = registries[i];
            if (!itemRegistries.ContainsKey(item.id))
                itemRegistries.Add(item.id, item);
            else
                throw new Exception(string.Format("Id {0} is registied", item.id));
        }
    }

    public GameObject GetItemPrefab(string id)
    {
        if (itemRegistries.TryGetValue(id, out ItemRegistry reg))
        {
            return reg.Prefab;
        }

        return null;
    }

    public ItemRegistry GetItemRegistry(InventoryData.ItemStack stack)  //获取物品注册信息
    {
        return GetItemRegistry(stack.id);
    }

    public ItemRegistry GetItemRegistry(string id)
    {
        if (itemRegistries.TryGetValue(id, out ItemRegistry reg))
        {
            return reg;
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

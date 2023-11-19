using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    HashSet<DroppedItem> itemInRange = new HashSet<DroppedItem>();
    

    // Start is called before the first frame update
    [SerializeField] PlayerController owner;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            DroppedItem i = collider.gameObject.GetComponent<DroppedItem>();
            if (i)
            {
                itemInRange.Add(i);
            }
        }
    }

    private void Update()
    {
        SwitchAndCollector();
    }

    public int selected = 1;
    void SwitchAndCollector()
    {
        selected += Input.GetKeyDown(KeyCode.Tab) ? 1 : 0;
        if (selected > itemInRange.Count) selected = 1;

        if (Input.GetKeyDown(KeyCode.F))
        {
            int idx = 0;

            itemInRange.RemoveWhere((i) =>
            {
                idx++;
                if (idx == selected)
                {
                    bool sucess = owner.Inventory.PushItem(i);
                    if (sucess) itemInRange.Remove(i);
                    i.OnCollect(owner, sucess);
                    return sucess;
                }
                return false;
            });
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            DroppedItem i = collider.gameObject.GetComponent<DroppedItem>();
            if (i)
            {
                itemInRange.Remove(i);
            }
        }
    }

    Rect win_rect = new Rect(Screen.width/2 + 100, Screen.height/2, 200, 150);
    private void OnGUI()
    {
        if (itemInRange.Count > 0) GUILayout.Window(1000, win_rect, WindowFunc, "拾取");
    }

    Vector2 scroll = new Vector2();
    private void WindowFunc(int id)
    {
        if (id != 1000) return; // 安全

        GUILayout.BeginVertical();
        scroll = GUILayout.BeginScrollView(scroll);

        int idx = 0;

        itemInRange.RemoveWhere((i) =>
        {
            idx++;
            if (GUILayout.Button( string.Format(idx == selected ? "> {0} <" : "{0}", i.Name) ) )
            {
                bool sucess = owner.Inventory.PushItem(i);
                if (sucess) itemInRange.Remove(i);
                i.OnCollect(owner, sucess);
                return sucess;
            }
            return false;
        });

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
}

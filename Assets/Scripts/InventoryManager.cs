using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // 單例模式
    public List<Item> Items = new List<Item>(); // 道具列表

    public Transform ItemContent; // UI 上道具欄的位置
    public GameObject InventoryItem; // 預製物件，用於顯示道具

    private void Awake()
    {
        // 確保只有一個 InventoryManager 實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 防止被銷毀
            if (ItemContent != null)
            {
                DontDestroyOnLoad(ItemContent.root);
            }
        }
        else
        {
            Destroy(gameObject); // 避免重複的 InventoryManager
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // 清除道具欄的現有內容
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // 添加新的道具到道具欄中
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
}
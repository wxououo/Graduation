using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent; // 道具欄 UI 元件
    public GameObject InventoryItem; // 預製件

    private void Awake()
    {
        // 檢查是否已有實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持物件不被銷毀
        }
        else
        {
            // 如果已有實例，刪除重複的物件
            Destroy(gameObject);
            return;
        }

        // 確保 ItemContent 的根物件也不會被銷毀
        if (ItemContent != null)
        {
            DontDestroyOnLoad(ItemContent.root.gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 當場景切換後，檢查是否需要重新更新 UI
        if (scene.name == "A") // 假設 Inventory 的原場景為 "A"
        {
            // 確保 UI 被正確初始化
            if (ItemContent == null)
            {
                ItemContent = GameObject.Find("ItemContent")?.transform;
            }

            // 刷新顯示道具欄
            ListItems();
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        ListItems();
    }

    public void ListItems()
    {
        if (ItemContent == null)
        {
            Debug.LogError("ItemContent is missing!");
            return;
        }

        // 清空現有的道具顯示
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // 添加道具
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

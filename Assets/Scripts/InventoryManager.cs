using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // ��ҼҦ�
    public List<Item> Items = new List<Item>(); // �D��C��

    public Transform ItemContent; // UI �W�D���檺��m
    public GameObject InventoryItem; // �w�s����A�Ω���ܹD��

    private void Awake()
    {
        // �T�O�u���@�� InventoryManager ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ����Q�P��
            if (ItemContent != null)
            {
                DontDestroyOnLoad(ItemContent.root);
            }
        }
        else
        {
            Destroy(gameObject); // �קK���ƪ� InventoryManager
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
        // �M���D���檺�{�����e
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // �K�[�s���D���D���椤
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
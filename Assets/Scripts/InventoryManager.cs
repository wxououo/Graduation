using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent; // �D���� UI ����
    public GameObject InventoryItem; // �w�s��

    private void Awake()
    {
        // �ˬd�O�_�w�����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �O�����󤣳Q�P��
        }
        else
        {
            // �p�G�w����ҡA�R�����ƪ�����
            Destroy(gameObject);
            return;
        }

        // �T�O ItemContent ���ڪ���]���|�Q�P��
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
        // �����������A�ˬd�O�_�ݭn���s��s UI
        if (scene.name == "A") // ���] Inventory ��������� "A"
        {
            // �T�O UI �Q���T��l��
            if (ItemContent == null)
            {
                ItemContent = GameObject.Find("ItemContent")?.transform;
            }

            // ��s��ܹD����
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

        // �M�Ų{�����D�����
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // �K�[�D��
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

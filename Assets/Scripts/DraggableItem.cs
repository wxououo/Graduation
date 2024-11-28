using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Item item;
    private Camera playerCamera;
    private bool isDragging = false;

    private void Start()
    {
        playerCamera = Camera.main; // 獲取主攝影機
    }

    public void Initialize(Item item)
    {
        this.item = item;
        // 添加 collider 來偵測點擊
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true; // 使其成為觸發器
    }

    private void Update()
    {
        // 檢查右鍵點擊開始拖動
        if (Input.GetMouseButtonDown(1)) // 1 代表右鍵
        {
            isDragging = true;
        }
        
        // 檢查右鍵釋放結束拖動
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // 拖動過程
        if (isDragging)
        {
            // 獲取鼠標位置並更新道具位置
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 100f; // 設定與攝影機的距離
            Vector3 worldPosition = playerCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        // 當玩家點擊物品時，將其放回物品欄
        InventoryManager.Instance.Add(item);
        Destroy(gameObject); // 銷毀這個生成的物品
    }
}
